using System;
using RDTools.Runtime;
using UnityEngine;

namespace LikeAGTA.Characters.SaveData
{
    [Serializable]
    public class PlayerData
    {
        public SerializedVector3 Position;
        [field: SerializeField] public int Health { get; set; }
        [field: SerializeField] public int Money { get; set; }

        public int MaxHealth => 100;

        public PlayerData(Vector3 position, int health, int money)
        {
            Position = new SerializedVector3(position);
            Health = health;
            Money = money;
        }
    }
}