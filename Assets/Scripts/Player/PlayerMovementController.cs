using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.PlayerCombat;
using UnityEngine.InputSystem;

namespace GameJam.Player
{
	public class PlayerMovementController : MonoBehaviour
	{
		[SerializeField] private CharacterControllerSettings _playerSettings;
		[SerializeField] private ChargeManager _chargeManager;
		[SerializeField] private Transform _groundCheck;
		[SerializeField] private LayerMask _groundMask;

		private CharacterController _characterController;

		private Vector3 _jumpVelocity;
		private float _groundDistance = 0.4f;
		private bool _isGrounded;
		private Vector3 _movement;

		private void Awake()
		{
			_characterController = GetComponent<CharacterController>();
		}

		void Update()
		{
			PlayerRunning();

			#region Gravity

			_isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

			if (_isGrounded && _jumpVelocity.y < 0)
			{
				_jumpVelocity.y = 0;
			}

			_jumpVelocity.y += _playerSettings.Gravity * Time.deltaTime;

			_characterController.Move(_jumpVelocity * Time.deltaTime);

			#endregion

			#region Movement


			//float x = Input.GetAxis("Horizontal");
			//float z = Input.GetAxis("Vertical");
			//Vector3 move = transform.right * x + transform.forward * z;

			_characterController.Move(_movement * _playerSettings.CurrentSpeed * Time.deltaTime);

			#endregion
		}

		private void PlayerRunning()
		{
			if (Input.GetKey(KeyCode.LeftShift))
			{
				_chargeManager.EnableStamina();
			}

			else if (Input.GetKeyUp(KeyCode.LeftShift))
			{
				_chargeManager.DisableRun();
			}
		}

		public void OnMovement(InputAction.CallbackContext movementInput)
		{
			Vector3 input = movementInput.ReadValue<Vector3>();
			_movement = transform.right * input.x + transform.forward * input.z;
		}

		public void OnJump(InputAction.CallbackContext jumpInput)
		{
			if (_isGrounded)
				_jumpVelocity.y += Mathf.Sqrt(_playerSettings.JumpHeight * -3.0f * _playerSettings.Gravity);
		}
	}
}
