using System;
using LikeAGTA.Characters.SaveData;
using UnityEngine;

namespace LikeAGTA.Systems.PickUpSystem
{
    public class PlayerPickup
    {
        private PlayerData _playerData;

        public void Setup(PlayerData playerData)
        {
            _playerData = playerData;
        }
        
        public event Action<int> OnMoneyChanged;
        public event Action<int> OnHealthChanged;
        
        public void CollectMoney(int amount)
        {
            _playerData.Money = Mathf.Clamp(_playerData.Money + amount, 0, int.MaxValue);
            OnMoneyChanged?.Invoke(_playerData.Money);
        }
        
        public void ChangeHealth(int amount)
        {
            _playerData.Health = Mathf.Clamp(_playerData.Health + amount, 0, _playerData.MaxHealth);
            OnHealthChanged?.Invoke(_playerData.Health);
        }
    }
}