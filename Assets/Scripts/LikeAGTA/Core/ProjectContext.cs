using System;
using System.Threading.Tasks;
using DI;
using RD_SimpleDI.Runtime.LifeCycle;
using RDTools.AutoAttach;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
namespace LikeAGTA.Core
{
    public class ProjectContext : MonoRunner
    {
        [SerializeField, Attach(Attach.Scene)] PlayerInput _playerInput;
        
        protected override async void BeforeAwake()
        {
            try
            {
                base.BeforeAwake();
                InitializeBindings();
                Subscribe();
                SetupDontDestroy();

                await LoadMainScene();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private void SetupDontDestroy()
        {
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(_playerInput.gameObject);
        }

        void InitializeBindings()
        {
            DIContainer.Instance.Bind(_playerInput);
            // Register global services and dependencies
            //container.Bind<IAds, AdsService>();
        }
        
        void Subscribe()
        {
            _playerInput.actions["Pause"].performed += OnPausePerformed;
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
            if (_playerInput)
                _playerInput.actions["Pause"].performed -= OnPausePerformed;
        }

        protected override void Delete()
        {
            base.Delete();
            Unsubscribe();
        }
    }
}