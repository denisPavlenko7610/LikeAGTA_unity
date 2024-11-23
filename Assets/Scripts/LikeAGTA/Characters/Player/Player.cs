using RDTools.AutoAttach;
using LikeAGTA.Systems.PickUpSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LikeAGTA.Characters
{
    public class Player : Character
    {
        [SerializeField, Attach] PlayerMovement _playerMovement;
        [SerializeField, Attach] Animator _playerAnimator;
        
        PlayerPickup _playerPickup;
        private InputAction _aimAction;
        private InputAction _attackAction;

        protected override void Initialize()
        {
            base.Initialize();
            _aimAction = InputSystem.actions.FindAction(ActionConstants.Aim);
            _attackAction = InputSystem.actions.FindAction(ActionConstants.Attack);
            
            _aimAction.performed += OnAimPerformed;
            _aimAction.canceled += OnAimCanceled;

            _attackAction.performed += OnAttackPerformed;
        }
        
        protected override void Delete()
        {
            base.Delete();
            _aimAction.performed -= OnAimPerformed;
            _aimAction.canceled -= OnAimCanceled;

            _attackAction.performed -= OnAttackPerformed;
        }

        private void OnAttackPerformed(InputAction.CallbackContext obj)
        {
            _playerAnimator.SetTrigger(AnimationConstants.Attack);
        }
        
        private void OnAimPerformed(InputAction.CallbackContext obj)
        {
            _playerAnimator.SetBool(AnimationConstants.Aim, true);
        }
        
        private void OnAimCanceled(InputAction.CallbackContext obj)
        {
            _playerAnimator.SetBool(AnimationConstants.Aim, false);
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