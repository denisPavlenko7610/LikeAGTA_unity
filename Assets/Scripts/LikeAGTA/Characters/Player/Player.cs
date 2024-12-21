using System;
using LikeAGTA.Characters.SaveData;
using LikeAGTA.Systems.PickUpSystem;
using RD_Save.Runtime;
using RD_SimpleDI.Runtime.DI.Attributes;
using RDTools.AutoAttach;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LikeAGTA.Characters
{
    public class Player : Character
    {
        [SerializeField, Attach] private PlayerMovement _playerMovement;
        
        private PlayerPickup _playerPickup;
        
        public Action OnPlayerAiming;
        public Action OnPlayerStopAiming;
        public Action OnPlayerShoot;
        
        private InputAction _aimAction;
        private InputAction _attackAction;
        private PlayerData _playerData;
        
        [Inject]
        SaveSystem _saveSystem;
        
        protected override void Initialize()
        {
            base.Initialize();
            _playerPickup = new PlayerPickup();
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

        private void Start()
        {
            _playerPickup.Setup(_playerData);
        }

        private void OnTriggerEnter(Collider other)
        {
            PickupObject(other);
        }

        protected override void Delete()
        {
#if !UNITY_EDITOR
            base.Delete();

            if (!Application.isPlaying)
            {
                Save();
            }
#endif
        }
        
#if UNITY_EDITOR
        private void OnApplicationQuit()
        {
            Save();
        }
#endif
        
        public void SetupPlayerData(PlayerData playerData)
        {
            _playerData = playerData;
        }

        public PlayerData GetPlayerData() => _playerData;
        public PlayerPickup GetPlayerPickup() => _playerPickup;
        
        private void OnAttackPerformed(InputAction.CallbackContext obj) => OnPlayerShoot?.Invoke();
        private void OnAimPerformed(InputAction.CallbackContext obj) => OnPlayerAiming?.Invoke();
        private void OnAimCanceled(InputAction.CallbackContext obj) => OnPlayerStopAiming?.Invoke();

        private void Save()
        {
            _playerData.Position = transform.position;
            _saveSystem.Save(_playerData);
        }
        
        private void PickupObject(Collider other)
        {
            if (!other.TryGetComponent(out IPickup pickup))
                return;
            
            pickup.OnPickup(_playerPickup);
            Destroy(other.gameObject);
        }
    }
}