using DI.Attributes;
using LikeAGTA.Systems.PickUpSystem;
using RD_Tween.Runtime.LifeCycle;
using TMPro;
using UnityEngine;

namespace LikeAGTA.Characters.UI
{
    public class HUD : MonoRunner
    {
        [SerializeField] TextMeshProUGUI _moneyText;
        [SerializeField] TextMeshProUGUI _heartText;

        private Player _player;
        
        [Inject]
        private void Construct(Player player)
        {
            _player = player;
        }
        
        private void OnEnable()
        {
            Subscribe();
        }

        private void OnDisable()
        {
            Unsubscribe();
        }
        
        private void Subscribe()
        {
            PlayerPickup playerPickup = _player.GetPlayerPickup();
            playerPickup.OnHealthChanged += UpdatePlayerHealth;
            playerPickup.OnMoneyChanged += UpdatePlayerMoney;
        }

        private void Unsubscribe()
        {
            PlayerPickup playerPickup = _player.GetPlayerPickup();
            if (playerPickup != null)
            {
                playerPickup.OnHealthChanged -= UpdatePlayerHealth;
                playerPickup.OnMoneyChanged -= UpdatePlayerMoney;
            }
        }

        private void UpdatePlayerMoney(int money)
        {
            _moneyText.text = $"${money}";
        }

        private void UpdatePlayerHealth(int hearts)
        {
            _heartText.text = $"Health: {hearts}";
        }
    }
}