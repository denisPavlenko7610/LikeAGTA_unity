using DI;
using LikeAGTA.Characters;
using LikeAGTA.Characters.Data;
using LikeAGTA.Characters.UI;
using LikeAGTA.Factory;
using RD_Tween.Runtime.LifeCycle;
using UnityEngine;
using UnityEngine.Serialization;

namespace LikeAGTA.Core
{
    public class SceneContext : MonoRunner
    {
        [SerializeField] PlayerDataSO _playerData;
        [FormerlySerializedAs("_hud")] [SerializeField] HUD _hudPrefab;

        protected override void BeforeAwake()
        {
            base.BeforeAwake();
            InitializeBindings();
            SpawnInitialCharacters();
            Instantiate(_hudPrefab);
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