using DI;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace LikeAGTA.Core
{
    public class ProjectContext : MonoBehaviour
    {
        void Awake()
        {
            InitializeBindings();
            DontDestroyOnLoad(gameObject);

            SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(0).buildIndex + 1);
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