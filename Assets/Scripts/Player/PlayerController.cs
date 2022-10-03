using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.PlayerCombat;

namespace GameJam.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private CharacterController _controller;
        [SerializeField] private CharacterControllerSettings _playerSettings;
        [SerializeField] private ChargeManager _chargeManager;
        [SerializeField] private PlayerStats _hud;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private LayerMask _groundMask;

        private Vector3 _velocity;
        private float _groundDistance = 0.4f;
        private bool _isGrounded;

        void Update()
        {
            #region Run

            if (Input.GetKey(KeyCode.LeftShift))
            {
                _chargeManager.EnableStamina();
            }

            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                _chargeManager.DisableRun();
            }
            #endregion


            #region Jump

            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                _velocity.y += Mathf.Sqrt(_playerSettings.JumpHeight * -3.0f * _playerSettings.Gravity);
            }

            #endregion            


            #region MoveSettings

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            _controller.Move(move * _playerSettings.CurrentSpeed * Time.deltaTime);

            #endregion


            #region Gravity

            _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = 0;
            }

            _velocity.y += _playerSettings.Gravity * Time.deltaTime;

            _controller.Move(_velocity * Time.deltaTime);

            #endregion
        }
    }   
}
