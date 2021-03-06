using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : Singleton<StarterAssetsInputs>
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool aim;
		public bool atack;
		public Vector2 changeWeapon;
		public bool equipment;
		public bool interact;
		public bool quests;
		public bool skillTree;

		[Header("Movement Settings")]
		public bool analogMovement;

#if !UNITY_IOS || !UNITY_ANDROID
		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
#endif

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
		public void OnAim(InputValue value)
		{
			AimInput(value.isPressed);
		}
		public void OnAtack(InputValue value)
		{
			AtackInput(value.isPressed);
		}
		public void OnChangeWeapon(InputValue value)
		{
			WeaponChangeInput(value.Get<Vector2>());
		}
		public void OnInventory(InputValue value)
		{
			InventoryInput(value.isPressed);
		}
		public void OnInteract(InputValue value)
		{
			InteractInput(value.isPressed);
		}
		public void OnQuests(InputValue value)
		{
			QuestsInput(value.isPressed);
		}
		public void OnSkillTree(InputValue value)
		{
			SkillTreeInput(value.isPressed);
		}
#else
	// old input sys if we do decide to have it (most likely wont)...
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
		public void AimInput(bool newAimState)
		{
			aim = newAimState;
		}
		public void AtackInput(bool newAtackState)
		{
			atack = newAtackState;
		}
		public void WeaponChangeInput(Vector2 newWeaponChangeState)
		{
			changeWeapon = newWeaponChangeState;
		}
		public void InventoryInput(bool newInventory)
		{
			equipment = newInventory;
		}
		public void InteractInput(bool newInteractState)
		{
			interact = newInteractState;
		}
		public void QuestsInput(bool newQuestState)
		{
			quests = newQuestState;
		}
		public void SkillTreeInput(bool newSkillTreeState)
		{
			skillTree = newSkillTreeState;
		}

#if !UNITY_IOS || !UNITY_ANDROID

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		public void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

#endif

	}
	
}