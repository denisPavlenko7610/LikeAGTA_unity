using System;
using System.Threading.Tasks;
using _Packages.RD_Save.Runtime;
using _Packages.RD_SimpleDI.Runtime.LifeCycle;
using DI;
using RD_SimpleDI.Runtime;
using RD_Tween.Runtime;
using RDTools.AutoAttach;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
namespace LikeAGTA.Core
{
    public class ProjectContext : MonoBehaviour
    {
        [SerializeField] private SaveFormat _saveFormat;
        [SerializeField] private string _saveFilePath;

        [SerializeField, Attach(Attach.Scene)] RunnerUpdater _runnerUpdater;
        [SerializeField, Attach(Attach.Scene)] TweenUpdater _tweenUpdater;
        
        private InputAction _pauseAction;
        private SaveSystem _saveSystem;

        protected async void Awake()
        {
            try
            {
                InitializeBindings();
                Subscribe();
                SetUnityLogStatus();
                SetupDontDestroy();
                await LoadMainScene();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
       
        private void SetupDontDestroy()
        {
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(_tweenUpdater);
            DontDestroyOnLoad(_runnerUpdater);
        }

        void InitializeBindings()
        {
            _saveSystem = new SaveSystem(SaveSystemFactory.GetSerializer(_saveFormat), _saveFormat, _saveFilePath);
            DIContainer.Instance.Bind(_saveSystem);
        }
        
        void Subscribe()
        {
            _pauseAction = InputSystem.actions.FindAction("Pause");
            _pauseAction.performed += OnPausePerformed;
        }
        
        void SetUnityLogStatus() => Debug.unityLogger.logEnabled = Debug.isDebugBuild;

        private async Task LoadMainScene()
        {
            AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync("GameScene");
            while (!loadSceneAsync.isDone)
            {
                await Task.Yield();
            }
        }
        
        public static T Resolve<T>() => DIContainer.Instance.Resolve<T>();
        
        private void OnPausePerformed(InputAction.CallbackContext context) => GameState.TogglePause();

        void Unsubscribe()
        {
            if (_pauseAction != null)
            {
                _pauseAction.performed -= OnPausePerformed;
            }
        }

        protected void OnDestroy()
        {
            Unsubscribe();
        }
    }
}