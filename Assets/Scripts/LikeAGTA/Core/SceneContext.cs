using DI;
using LikeAGTA.Characters;
using LikeAGTA.Characters.Data;
using LikeAGTA.Characters.UI;
using LikeAGTA.Factory;
using RD_SimpleDI.Runtime.LifeCycle;
using UnityEngine;

namespace LikeAGTA.Core
{
    public class SceneContext : MonoRunner
    {
        [SerializeField] PlayerDataSO _playerData;
        [SerializeField] HUD _hudPrefab;
        
        protected void Awake()
        {
            InitializeBindings();
            SpawnInitialCharacters();
            Instantiate(_hudPrefab);
#if UNITY_EDITOR
            DIContainer.Instance.ValidateRegistrations();
#endif
        }

        void InitializeBindings()
        {
            DIContainer.Instance.Bind<ICharacterFactory, CharacterFactory>();
        }

        void SpawnInitialCharacters()
        {
            ICharacterFactory characterFactory = DIContainer.Instance.Resolve<ICharacterFactory>();
            Player player = characterFactory.SpawnCharacter(_playerData.Prefab, _playerData.SpawnPosition,
                Quaternion.identity, true);
        }
    }
}