using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.Player;

namespace GameJam.PlayerCombat
{
    public class ChargeManager : MonoSingleton<ChargeManager>
    {
        [SerializeField] private CharacterControllerSettings _playerSettings;
        [HideInInspector] public bool canDefense;
        
        private PlayerStats _playerStats;
        private bool _canRun;
        private float _speed;


        private void Start()
        {
            _playerStats = GetComponent<PlayerStats>();
            _speed = _playerSettings.CurrentSpeed;
        }

        #region Projectile
        public void EnableProjectile()
        {
            _playerSettings.Charge -= _playerSettings.ProjectileAmount;
            if (_playerSettings.Charge <= 0) _playerSettings.Charge = 0;          
            ChargeSystem();
        }

        #endregion

        #region Shield
        public void EnableShield(GameObject _fireShield)
        {
            if (_playerSettings.Charge > 0)
            {
                _fireShield.gameObject.SetActive(true);
                canDefense = true;
                _playerSettings.Charge -= _playerSettings.ShieldAmount * Time.deltaTime;

                if (_playerSettings.Charge <= 0)
                {
                    _playerSettings.Charge = 0;
                    DisableShield(_fireShield);
                }
            }

            ChargeSystem();
        }
        public void DisableShield(GameObject _fireShield)
        {
            _fireShield.gameObject.SetActive(false);
            canDefense = false;
            ChargeSystem();
        }

        #endregion

        #region Stamina
        public void EnableStamina()
        {
            if (_playerSettings.Stamina > 0)
            {
                _canRun = true;
                _playerSettings.Stamina -= _playerSettings.StaminaAmount * Time.deltaTime;

                if (_canRun)
                {
                    _playerSettings.CurrentSpeed = Mathf.MoveTowards(_playerSettings.CurrentSpeed, 
                    _playerSettings.MaxSpeed, _playerSettings.AccelerationSpeed * Time.deltaTime);                                       
                }
                
                if (_playerSettings.Stamina <= 0 || !_canRun) DisableRun();             
            }

            StaminaSystem();
        }

        public void DisableRun()
        {
            _canRun = false;
            _playerSettings.CurrentSpeed = _speed;
            StaminaSystem();
        }

        #endregion

        #region Reload Bar
        public void ChargeSystem()
        {
            _playerStats.ChargeControl(true);           

            if (_playerSettings.Charge < _playerSettings.MaxCharge) _playerStats.ChargeControl(false);         
        }

        public void StaminaSystem()
        {
            _playerStats.StaminaControl(true);

            if (_playerSettings.Stamina < _playerSettings.MaxStamina) _playerStats.StaminaControl(false);
        }
        #endregion
    }
}

