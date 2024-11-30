using RD_SimpleDI.Runtime.LifeCycle;
using RDTools.AutoAttach;
using UnityEngine;

namespace LikeAGTA.Characters
{
    public class PlayerAnimator : MonoRunner
    {
        [SerializeField, Attach] Animator _playerAnimator;
        [SerializeField, Attach] Player _player;
        
        protected void OnEnable()
        {
            _player.OnPlayerShoot += ShootAnimation;
            _player.OnPlayerAiming += AimingAnimation;
            _player.OnPlayerStopAiming += StopAimingAnimation;
        }

        protected void OnDisable()
        {
            _player.OnPlayerShoot -= ShootAnimation;
            _player.OnPlayerAiming -= AimingAnimation;
            _player.OnPlayerStopAiming -= StopAimingAnimation;
        }

        private void ShootAnimation()
        {
            _playerAnimator.SetTrigger(AnimationConstants.Attack);
        }
        
        private void AimingAnimation()
        {
            _playerAnimator.SetBool(AnimationConstants.Aim, true);
        }

        private void StopAimingAnimation()
        {
            _playerAnimator.SetBool(AnimationConstants.Aim, false);
        }
    }
}