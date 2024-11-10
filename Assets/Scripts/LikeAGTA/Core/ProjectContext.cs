using System.Threading.Tasks;
using DI;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace LikeAGTA.Core
{
    public class ProjectContext : MonoBehaviour
    {
        async void Awake()
        {
            InitializeBindings();
            DontDestroyOnLoad(gameObject);

            await LoadMainScene();
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

        void InitializeBindings()
        {
            var container = DIContainer.Instance;

            // Register global services and dependencies
            //container.Bind<IAds, AdsService>();
        }

        public static T Resolve<T>() => DIContainer.Instance.Resolve<T>();
    }
}