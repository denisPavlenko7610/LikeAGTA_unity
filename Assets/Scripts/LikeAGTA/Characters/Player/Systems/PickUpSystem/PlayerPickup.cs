using System;

namespace LikeAGTA.Systems.PickUpSystem
{
    public class PlayerPickup
    {
        //temp
        int _money = 0;
        int _healthCount = 100;
        
        public event Action<int> OnMoneyChanged;
        public event Action<int> OnHealthChanged;
        
        public void CollectMoney(int amount)
        {
            _money += amount;
            OnMoneyChanged?.Invoke(_money);
        }
        
        public void CollectHeart(int amount)
        {
            _healthCount += amount;
            OnHealthChanged?.Invoke(_healthCount);
        }
    }
}