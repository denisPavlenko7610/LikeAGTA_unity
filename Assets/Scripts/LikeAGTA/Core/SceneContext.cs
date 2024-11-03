using DI;
using DI.Interfaces;
using UnityEngine;
namespace LikeAGTA.Core
{
    public class SceneContext : MonoBehaviour
    {
        void Awake()
        {
            InitializeBindings();
            injectDependencies();
        }
    
        void InitializeBindings()
        {
            // DIContainer.Instance.Bind<IAudioService, VideoService>(); //Bind non mono
        }
    
        void injectDependencies()
        {
            foreach (MonoBehaviour monoBehaviour in FindObjectsOfType<MonoBehaviour>(true))
            {
                DIInitializer.Instance.InjectDependencies(monoBehaviour);
                if (monoBehaviour is IInitializable initializable)
                {
                    initializable.Initialize();
                }
            }
        }
    }
}