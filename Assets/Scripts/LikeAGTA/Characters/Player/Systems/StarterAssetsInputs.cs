using RD_SimpleDI.Runtime;
using RD_SimpleDI.Runtime.LifeCycle;
using RD_SimpleDI.Runtime.LifeCycle.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoRunner, IPause
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		void Pause()
		{
			move = Vector2.zero;
			look = Vector2.zero;
		}

		public void OnLook(InputValue value)
		{
			if (!cursorInputForLook)
				return;
			
			LookInput(value.Get<Vector2>());
		}
		
		public void OnMove(InputValue value)
		{
			if (GameState.IsPaused)
				return;
			
			 MoveInput(value.Get<Vector2>());
		}

		public void OnJump(InputValue value)
		{
			if (GameState.IsPaused)
				return;
			
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			if (GameState.IsPaused)
				return;
			
			SprintInput(value.isPressed);
		}

		public void MoveInput(Vector2 newMoveDirection)
		{
			if (GameState.IsPaused)
				return;
			
			move = newMoveDirection;
		}

		public void LookInput(Vector2 newLookDirection)
		{
			if (GameState.IsPaused)
				return;
			
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			if (GameState.IsPaused)
				return;
			
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			if (GameState.IsPaused)
				return;
			
			sprint = newSprintState;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState 
				? CursorLockMode.Locked 
				: CursorLockMode.None;
		}

		void IPause.Pause()
		{
			Pause();
		}
	}
}