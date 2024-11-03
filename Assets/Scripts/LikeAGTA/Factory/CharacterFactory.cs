using DI;
using LikeAGTA.Characters;
using UnityEngine;

namespace LikeAGTA.Factory
{
    public class CharacterFactory : ICharacterFactory
    {
        public T SpawnCharacter<T>(T prefab, Vector3 position, Quaternion rotation) where T : Character
        {
            T characterInstance = DIContainer.Instance.InstantiateAndInject(prefab, position, rotation);
            return characterInstance;
        }
    }
}