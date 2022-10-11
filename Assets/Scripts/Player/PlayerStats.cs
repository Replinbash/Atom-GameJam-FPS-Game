using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.Player
{
    public class PlayerStats : CharacterStats
    {
        [SerializeField] private PlayerSettings _playerSettings;
        [SerializeField] private PlayerHUD _hud;

        private int _chargeRate;
        private Coroutine _reloadCharge;
        private Coroutine _reloadStamina;
        private WaitForSeconds _coroutineRange;

        private void Start() => InitVariables();

        private void Update()
        {
            _hud.UptadeCharge((int)_playerSettings.Charge, (int)_playerSettings.MaxCharge);
            _hud.UptadeStamina((int)_playerSettings.Stamina, (int)_playerSettings.MaxStamina);
            _hud.UptadeHealth(health, maxHealth);
        }

        public override void InitVariables()
        {
            base.InitVariables();
            SetHealthTo(maxHealth);

            // hud settings
            maxHealth = _playerSettings.MaxHealth;
            _playerSettings.Charge = _playerSettings.MaxCharge;
            _playerSettings.Stamina = _playerSettings.MaxStamina;

            // coroutine settings
            _chargeRate = 5;
            _coroutineRange = new WaitForSeconds(0.6f);
        }

        #region ChargeSystem
        public void ChargeControl(bool isActive)
        {
            if (!isActive) _reloadCharge = StartCoroutine(ReloadCharge(3));            

            if (isActive) if (_reloadCharge != null) StopCoroutine(_reloadCharge);       
        }
        
        private IEnumerator ReloadCharge(int wait)
        {
            yield return new WaitForSeconds(wait);

            while (_playerSettings.Charge < _playerSettings.MaxCharge)
            {
                _playerSettings.Charge += _playerSettings.MaxCharge / _chargeRate;

                if (_playerSettings.Charge >= _playerSettings.MaxCharge)
                {
                    _playerSettings.Charge = _playerSettings.MaxCharge;
                    StopCoroutine(_reloadCharge);
                }
                yield return _coroutineRange;
            }
        }
        #endregion

        #region StaminaSystem
        public void StaminaControl(bool isActive)
        {
            if (!isActive) _reloadStamina = StartCoroutine(ReloadStamina(3));

            if (isActive) if (_reloadStamina != null) StopCoroutine(_reloadStamina);
        }

        private IEnumerator ReloadStamina(int wait)
        {
            yield return new WaitForSeconds(wait);

            while (_playerSettings.Stamina < _playerSettings.MaxStamina)
            {
                _playerSettings.Stamina += _playerSettings.MaxStamina / _chargeRate;

                if (_playerSettings.Stamina >= _playerSettings.MaxStamina)
                {
                    _playerSettings.Stamina = _playerSettings.MaxStamina;
                    StopCoroutine(_reloadStamina);
                }
                yield return _coroutineRange;
            }
        }
        #endregion
    }

}
