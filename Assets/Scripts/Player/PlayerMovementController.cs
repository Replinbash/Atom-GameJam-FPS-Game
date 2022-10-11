using UnityEngine;
using GameJam.PlayerCombat;
using UnityEngine.InputSystem;

namespace GameJam.Player
{
	public class PlayerMovementController : MonoBehaviour
	{
		[SerializeField] private PlayerSettings _playerSettings;
		[SerializeField] private InputReader _inputReader;
		[SerializeField] private Transform _groundCheck;
		[SerializeField] private LayerMask _groundMask;

		private CharacterController _characterController;

		private Vector3 _jumpVelocity;
		private float _groundDistance = 0.2f;
		private bool _isGrounded;
		private bool isRunning;
		protected float playerSpeed;

		private void Awake()
		{
			_characterController = GetComponent<CharacterController>();
		}

		private void OnEnable()
		{
			_inputReader.JumpEvent += ProcessOnJump;
			_inputReader.StartedRunning += StartRunning;
			_inputReader.StoppedRunning += StopRunning;
		}

		private void OnDisable()
		{
			_inputReader.JumpEvent -= ProcessOnJump;
			_inputReader.StartedRunning -= StartRunning;
			_inputReader.StoppedRunning -= StopRunning;
		}

		private void Start()
		{
			playerSpeed = _playerSettings.CurrentSpeed;
		}

		void Update()
		{
			OnJump();
			OnMovement();
			HandleRunning();
		}
	
		public void StartRunning() => isRunning = true;
		public void StopRunning() => isRunning = false;

		public void ProcessOnJump()
		{
			if (_isGrounded)
				_jumpVelocity.y += Mathf.Sqrt(_playerSettings.JumpHeight * -3.0f * _playerSettings.Gravity);
		}

		private void OnJump()
		{
			_isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

			if (_isGrounded && _jumpVelocity.y < 0)
			{
				_jumpVelocity.y = 0;
			}

			_jumpVelocity.y += _playerSettings.Gravity * Time.deltaTime;

			_characterController.Move(_jumpVelocity * Time.deltaTime);
		}

		private void OnMovement()
		{
			float x = Input.GetAxis("Horizontal");
			float z = Input.GetAxis("Vertical");
			Vector3 move = transform.right * x + transform.forward * z;
			_characterController.Move(move * _playerSettings.CurrentSpeed * Time.deltaTime);
		}

		public void HandleRunning()
		{
			if (isRunning)
			{
				if (_playerSettings.Stamina > 0)
				{
					_playerSettings.Stamina -= _playerSettings.StaminaAmount * Time.deltaTime;
					_playerSettings.CurrentSpeed = Mathf.MoveTowards(_playerSettings.CurrentSpeed,
					_playerSettings.MaxSpeed, _playerSettings.AccelerationSpeed * Time.deltaTime);
				}

				if (_playerSettings.Stamina <= 0)
				{
					_playerSettings.Stamina = 0;
				}
			}

			if (!isRunning)
			{
				_playerSettings.CurrentSpeed = playerSpeed;
			}
		}
	}
}
