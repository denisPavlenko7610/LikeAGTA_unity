using System;
using DI.Attributes;
using DI.Interfaces;
using LikeAGTA.Characters.UI;
using RDTools.AutoAttach;
using LikeAGTA.Systems.PickUpSystem;
using UnityEngine;

namespace LikeAGTA.Characters
{
    public class Player : Character, IInitializable
    {
        [SerializeField, Attach] PlayerMovement _playerMovement;
        
        PlayerPickup _playerPickup;

        public override void Initialize()
        {
            base.Initialize();
            _playerPickup = new PlayerPickup();
        }

        private void OnTriggerEnter(Collider other)
        {
            PickupObject(other);
        }

        public PlayerPickup GetPlayerPickup() => _playerPickup;

        private void PickupObject(Collider other)
        {
            if (other.TryGetComponent(out IPickup pickup))
            {
                pickup.OnPickup(_playerPickup);
                Destroy(other.gameObject);
            }
        }
    }
}