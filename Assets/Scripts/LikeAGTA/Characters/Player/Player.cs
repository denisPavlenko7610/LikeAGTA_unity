using RDTools.AutoAttach;
using LikeAGTA.Systems.PickUpSystem;
using UnityEngine;

namespace LikeAGTA.Characters
{
    public class Player : Character
    {
        [SerializeField, Attach] PlayerMovement _playerMovement;
        
        PlayerPickup _playerPickup;

        private void OnTriggerEnter(Collider other)
        {
            PickupObject(other);
        }

        public PlayerPickup GetPlayerPickup() => _playerPickup ??= new PlayerPickup();

        private void PickupObject(Collider other)
        {
            if (!other.TryGetComponent(out IPickup pickup))
                return;
            
            pickup.OnPickup(_playerPickup);
            Destroy(other.gameObject);
        }
    }
}