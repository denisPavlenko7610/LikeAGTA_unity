using DI;
using LikeAGTA.Characters;
using UnityEngine;

namespace LikeAGTA.Factory
{
    public class CharacterFactory : ICharacterFactory
    {
        public T SpawnCharacter<T>(T prefab, Vector3 position, Quaternion rotation, bool needBind) where T : Character
        {
            if (needBind)
            {
                return DIContainer.Instance.InstantiateAndBind(prefab, position, rotation);
            }
           
            return Object.Instantiate(prefab, position, rotation);
        }
    }
}