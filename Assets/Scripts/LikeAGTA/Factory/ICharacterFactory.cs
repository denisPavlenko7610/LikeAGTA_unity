using LikeAGTA.Characters;
using UnityEngine;

namespace LikeAGTA.Factory
{
    public interface ICharacterFactory
    {
        public T SpawnCharacter<T>(T prefab, Vector3 position, Quaternion rotation) where T : Character;
    }
}