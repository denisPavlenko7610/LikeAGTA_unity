using DI.Attributes;
using LikeAGTA.Systems.PickUpSystem;
using RD_SimpleDI.Runtime.LifeCycle;
using TMPro;
using UnityEngine;

namespace LikeAGTA.Characters.UI
{
    public class HUD : MonoRunner
    {
        [SerializeField] TextMeshProUGUI _moneyText;
        [SerializeField] TextMeshProUGUI _heartText;
        [SerializeField] TextMeshProUGUI _pauseText;

        private Player _player;

        [Inject]
        private void Construct(Player player)
        {
            _player = player;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _pauseText.enabled = false;
            
        }

        protected override void Appear()
        {
            base.Appear();
            Subscribe();
        }

        protected override void Disappear()
        {
            base.Disappear();
            Unsubscribe();
        }

        void Pause()
        {
            _pauseText.enabled = true;
        }

        void Resume()
        {
            _pauseText.enabled = false;
        }

        private void Subscribe()
        {
            PlayerPickup playerPickup = _player.GetPlayerPickup();
            playerPickup.OnHealthChanged += UpdatePlayerHealth;
            playerPickup.OnMoneyChanged += UpdatePlayerMoney;
            
            OnPause += Pause;
            OnResume += Resume;
        }

        private void Unsubscribe()
        {
            PlayerPickup playerPickup = _player.GetPlayerPickup();
            if (playerPickup != null)
            {
                playerPickup.OnHealthChanged -= UpdatePlayerHealth;
                playerPickup.OnMoneyChanged -= UpdatePlayerMoney;
            }
            
            OnPause -= Pause;
            OnResume -= Resume;
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