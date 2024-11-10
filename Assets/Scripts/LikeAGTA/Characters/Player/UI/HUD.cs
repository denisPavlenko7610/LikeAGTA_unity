using System;
using DI.Attributes;
using DI.Interfaces;
using LikeAGTA.Systems.PickUpSystem;
using TMPro;
using UnityEngine;

namespace LikeAGTA.Characters.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _moneyText;
        [SerializeField] TextMeshProUGUI _heartText;

        private Player _player;
        
        [Inject]
        private void Construct(Player player)
        {
            _player = player;
        }

        
        public void Start()
        {
            Subscribe();
        }
        
        private void OnDestroy()
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