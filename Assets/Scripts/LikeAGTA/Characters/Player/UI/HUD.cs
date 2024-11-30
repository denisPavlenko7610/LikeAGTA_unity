using _Packages.RD_SimpleDI.Runtime.LifeCycle.Interfaces;
using DI.Attributes;
using LikeAGTA.Systems.PickUpSystem;
using RD_SimpleDI.Runtime.LifeCycle;
using TMPro;
using UnityEngine;

namespace LikeAGTA.Characters.UI
{
    public class HUD : MonoRunner, IPause, IResume
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

        protected void OnEnable()
        {
            //base.Appear();
            Subscribe();
        }

        protected void OnDisable()
        {
            //base.Disappear();
            Unsubscribe();
        }

        public void Pause()
        {
            _pauseText.enabled = true;
        }

        public void Resume()
        {
            _pauseText.enabled = false;
        }

        private void Subscribe()
        {
            PlayerPickup playerPickup = _player.GetPlayerPickup();
            playerPickup.OnHealthChanged += UpdatePlayerHealth;
            playerPickup.OnMoneyChanged += UpdatePlayerMoney;
        }

        private void Unsubscribe()
        {
            PlayerPickup playerPickup = _player?.GetPlayerPickup();
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