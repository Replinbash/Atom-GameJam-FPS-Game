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
			_inputReader.DefenceCanceledEvent += StartReloadDefence;
			_inputReader.AttackEvent += StopReload;
			_inputReader.DefenceEvent += StopReload;
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			_inputReader.AttackCanceledEvent -= StartReloadAttack;
			_inputReader.DefenceCanceledEvent -= StartReloadDefence;
			_inputReader.AttackEvent -= StopReload;
			_inputReader.DefenceEvent -= StopReload;
		}

		private void StartReloadDefence() => _reloadMana = StartCoroutine(ReloadMana(3));
		private void StartReloadAttack() => _reloadMana = StartCoroutine(ReloadMana(3));

		private void StopReload()
		{
			if (_reloadMana != null)
				StopCoroutine(_reloadMana);
		}

		protected IEnumerator ReloadMana(int wait)
		{
			yield return new WaitForSeconds(wait);

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


