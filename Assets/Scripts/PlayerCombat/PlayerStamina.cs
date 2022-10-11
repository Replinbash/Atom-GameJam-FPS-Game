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
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			_inputReader.StoppedRunning -= StartReload;
		}

		private void StartReload() => StartCoroutine(ReloadStamina(3));

		protected IEnumerator ReloadStamina(int wait)
		{
			yield return new WaitForSeconds(wait);
			Debug.Log("Stamina");

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
