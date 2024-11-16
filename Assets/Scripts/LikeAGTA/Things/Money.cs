using LikeAGTA.Systems.PickUpSystem;
using RD_Tween.Runtime;
using RD_Tween.Runtime.LifeCycle;
using UnityEngine;

namespace LikeAGTA.Things
{
    public class Money : MonoRunner, IPickup
    {
        [SerializeField] private int moneyValue = 10;
        [SerializeField] private Transform rotatebleModel;

        protected override void Initialize()
        {
            base.Initialize();
            rotatebleModel.RotateByY(360f, 1f)
                .Loop(-1)
                .Play();
        }

        public void OnPickup(PlayerPickup playerPickup)
        {
            playerPickup.CollectMoney(moneyValue);
        }
    }
}