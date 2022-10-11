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

		private void Awake()
		{
			_characterController = GetComponent<CharacterController>();
		}

		private void OnEnable()
		{
			_inputReader.JumpEvent += OnJump;
		}

		private void OnDisable()
		{
			_inputReader.JumpEvent -= OnJump;
		}

		void Update()
		{
			Gravity();
			Move();
		}

		private void Gravity()
		{
			_isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

			if (_isGrounded && _jumpVelocity.y < 0)
			{
				_jumpVelocity.y = 0;
			}

			_jumpVelocity.y += _playerSettings.Gravity * Time.deltaTime;

			_characterController.Move(_jumpVelocity * Time.deltaTime);
		}

		private void Move()
		{
			float x = Input.GetAxis("Horizontal");
			float z = Input.GetAxis("Vertical");
			Vector3 move = transform.right * x + transform.forward * z;

			_characterController.Move(move * _playerSettings.CurrentSpeed * Time.deltaTime);
		}

		public void OnJump()
		{
			if (_isGrounded)
				_jumpVelocity.y += Mathf.Sqrt(_playerSettings.JumpHeight * -3.0f * _playerSettings.Gravity);
		}
	}
}
