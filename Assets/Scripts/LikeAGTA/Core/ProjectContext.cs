using System;
using System.Threading.Tasks;
using DI;
using RD_SimpleDI.Runtime.LifeCycle;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
namespace LikeAGTA.Core
{
    public class ProjectContext : MonoRunner
    {
        private InputAction _pauseAction;  
        
        protected override async void BeforeAwake()
        {
            try
            {
                base.BeforeAwake();
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
        }

        void InitializeBindings()
        {
            //DIContainer.Instance.Bind(_uiInput);
        }
        
        void Subscribe()
        {
            _pauseAction = InputSystem.actions.FindAction("Pause");
            _pauseAction.performed += OnPausePerformed;
        }
        
        void SetUnityLogStatus()
        {
#if UNITY_EDITOR
            Debug.unityLogger.logEnabled = true;
#else
            Debug.unityLogger.logEnabled = false;
#endif
        }
        
        private async Task LoadMainScene()
        {
            AsyncOperation loadEnvironmentTask = SceneManager.LoadSceneAsync("Environment", LoadSceneMode.Additive);
            AsyncOperation loadPlayerTask = SceneManager.LoadSceneAsync("Player", LoadSceneMode.Additive);
            
            while (!loadEnvironmentTask.isDone || !loadPlayerTask.isDone)
            {
                await Task.Yield();
            }

            await SceneManager.UnloadSceneAsync("Bootstrap");
        }
        
        public static T Resolve<T>() => DIContainer.Instance.Resolve<T>();
        
        private void OnPausePerformed(InputAction.CallbackContext context) => TogglePause();

        void Unsubscribe()
        {
            _pauseAction.performed -= OnPausePerformed;
        }

        protected override void Delete()
        {
            base.Delete();
            Unsubscribe();
        }
    }
}