using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.Player
{
    public class MouseLook : MonoBehaviour
    {        
        [SerializeField] private Transform _playerBody;
        [SerializeField] private CharacterControllerSettings _settings;

        private float _xRotation = 0f;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") * _settings.MouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * _settings.MouseSensitivity * Time.deltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            _playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}

