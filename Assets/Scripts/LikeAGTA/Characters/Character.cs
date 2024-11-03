using DI.Interfaces;
using UnityEngine;

namespace LikeAGTA.Characters
{
    public class Character : MonoBehaviour, ICharacter, IInitializable
    {
        public virtual void Initialize()
        {
            
        }
    }
}