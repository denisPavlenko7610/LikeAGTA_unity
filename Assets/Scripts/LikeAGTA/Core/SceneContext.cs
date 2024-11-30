using DI;
using LikeAGTA.Characters;
using LikeAGTA.Characters.Data;
using LikeAGTA.Characters.UI;
using LikeAGTA.Factory;
using UnityEngine;

namespace LikeAGTA.Core
{
    [DefaultExecutionOrder(-40)]
    public class SceneContext : MonoBehaviour
    {
        [SerializeField] private Transform _playerStarterPoint;
        [SerializeField] private PlayerDataSO _playerData;
        [SerializeField] private HUD _hudPrefab;
        
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
            Player player = characterFactory.SpawnCharacter(_playerData.Prefab, _playerStarterPoint.transform.position,
                Quaternion.identity, true);
        }
    }
}