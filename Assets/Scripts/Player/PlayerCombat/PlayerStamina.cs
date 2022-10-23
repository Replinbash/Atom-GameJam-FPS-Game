using System.Collections;
using UnityEngine;

namespace GameJam.PlayerCombat
{
	public class PlayerStamina : ChargeSkill
	{	
		protected override void OnEnable()
		{
			base.OnEnable();
			_inputReader.StoppedRunning += StartReload;
			_inputReader.StartedRunning += StopReload;
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			_inputReader.StoppedRunning -= StartReload;
			_inputReader.StartedRunning -= StopReload;
		}

		private void StartReload() => _reloadStamina = StartCoroutine(ReloadStamina(3));
		private void StopReload()
		{
			if (_reloadStamina != null)
			StopCoroutine(_reloadStamina);
		}

		protected IEnumerator ReloadStamina(int wait)
		{
			yield return new WaitForSeconds(wait);

			while (_playerSettings.Stamina < _playerSettings.MaxStamina)
			{
				_playerSettings.Stamina += _playerSettings.MaxStamina / _chargeRate;

				if (_playerSettings.Stamina >= _playerSettings.MaxStamina)
				{
					_playerSettings.Stamina = _playerSettings.MaxStamina;
					yield break;
				}
				yield return _coroutineRange;
			}
		}
	}

}
