using LikeAGTA.Systems.PickUpSystem;
using RD_Tween.Runtime;
using UnityEngine;

namespace LikeAGTA.Things
{
    public class Money : MonoBehaviour, IPickup
    {
        [SerializeField] private int moneyValue = 10;
        [SerializeField] private Transform rotatebleModel;
        
        void Start()
        {
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