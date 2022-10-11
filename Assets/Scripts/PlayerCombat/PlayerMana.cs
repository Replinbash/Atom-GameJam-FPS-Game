using System.Collections;
using UnityEngine;

namespace GameJam.PlayerCombat
{
	public class PlayerMana : ChargeSkill
	{
		protected override void OnEnable()
		{
			base.OnEnable();
			_inputReader.AttackCanceledEvent += StartReloadAttack;
			_inputReader.DefenceCanceledEvent += StartReload;
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			_inputReader.AttackCanceledEvent -= StartReloadAttack;
			_inputReader.DefenceCanceledEvent -= StartReload;
		}

		private void StartReload() => StartCoroutine(ReloadMana(3));
		private void StartReloadAttack() => StartCoroutine(ReloadMana(6));

		protected IEnumerator ReloadMana(int wait)
		{
			yield return new WaitForSeconds(wait);
			Debug.Log("Mana");

			while (_playerSettings.Mana < _playerSettings.MaxMana)
			{
				_playerSettings.Mana += _playerSettings.MaxMana / _chargeRate;

				if (_playerSettings.Mana >= _playerSettings.MaxMana)
				{
					_playerSettings.Mana = _playerSettings.MaxMana;
					yield break;
				}
				yield return _coroutineRange;
			}
		}
	}
}


