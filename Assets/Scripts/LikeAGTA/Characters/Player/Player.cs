using System;
using RDTools.AutoAttach;
using LikeAGTA.Systems.PickUpSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LikeAGTA.Characters
{
    public class Player : Character
    {
        [SerializeField, Attach] PlayerMovement _playerMovement;
        
        PlayerPickup _playerPickup;
        
        public Action OnPlayerAiming;
        public Action OnPlayerStopAiming;
        public Action OnPlayerShoot;
        
        private InputAction _aimAction;
        private InputAction _attackAction;
        
        protected override void Initialize()
        {
            base.Initialize();
            _aimAction = InputSystem.actions.FindAction(ActionConstants.Aim);
            _attackAction = InputSystem.actions.FindAction(ActionConstants.Attack);
        }

        private void OnEnable()
        {
            _aimAction.performed += OnAimPerformed;
            _aimAction.canceled += OnAimCanceled;
            _attackAction.performed += OnAttackPerformed;
        }

        protected void OnDisable()
        {
            _aimAction.performed -= OnAimPerformed;
            _aimAction.canceled -= OnAimCanceled;
            _attackAction.performed -= OnAttackPerformed;
        }

        private void OnAttackPerformed(InputAction.CallbackContext obj)
        {
            OnPlayerShoot?.Invoke();
        }
        
        private void OnAimPerformed(InputAction.CallbackContext obj)
        {
            OnPlayerAiming?.Invoke();
        }
        
        private void OnAimCanceled(InputAction.CallbackContext obj)
        {
           OnPlayerStopAiming?.Invoke();
        }

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