using System;
using DI;
using DI.Interfaces;
using LikeAGTA.Characters;
using LikeAGTA.Characters.Data;
using LikeAGTA.Characters.UI;
using LikeAGTA.Factory;
using UnityEngine;

namespace LikeAGTA.Core
{
    public class SceneContext : MonoBehaviour
    {
        [SerializeField] PlayerDataSO _playerData;
        [SerializeField] HUD _hud;
       
        void Awake()
        {
            InitializeBindings();
            SpawnInitialCharacters();
            Instantiate(_hud);
            InjectDependencies();
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
    
        void InjectDependencies()
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