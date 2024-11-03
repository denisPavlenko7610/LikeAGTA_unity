using DI;
using DI.Interfaces;
using LikeAGTA.Characters;
using LikeAGTA.Characters.Data;
using LikeAGTA.Factory;
using UnityEngine;

namespace LikeAGTA.Core
{
    public class SceneContext : MonoBehaviour
    {
        [SerializeField] PlayerDataSO _playerData; //temp
        [SerializeField] Transform _playerSpawnPosition; //temp
        void Awake()
        {
            InitializeBindings();
            SpawnInitialCharacters();
            injectDependencies();
        }
    
        void InitializeBindings()
        {
            DIContainer.Instance.Bind<ICharacterFactory, CharacterFactory>();
        }

        void SpawnInitialCharacters()
        {
            ICharacterFactory characterFactory = DIContainer.Instance.Resolve<ICharacterFactory>();
            Player player = characterFactory.SpawnCharacter(_playerData.Prefab, _playerSpawnPosition.transform.position, Quaternion.identity);
            DIContainer.Instance.Bind(player);
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