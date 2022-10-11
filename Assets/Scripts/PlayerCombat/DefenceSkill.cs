using System;
using UnityEngine;

namespace GameJam.PlayerCombat
{
	public class DefenceSkill : BaseSkill
	{
		[SerializeField] private GameObject _fireShield;
		[SerializeField] protected PlayerSettings _playerSettings;
		private bool _canDefense;

		public static event Action<bool> SetDamage;

		protected override void OnEnable()
		{
			base.OnEnable();
			_inputReader.DefenceEvent += Defending;
			_inputReader.DefenceCanceledEvent += Undefending;
			_inputReader.DefenceCanceledEvent += DisableShield;
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			_inputReader.DefenceEvent -= Defending;
			_inputReader.DefenceCanceledEvent -= Undefending;
			_inputReader.DefenceCanceledEvent -= DisableShield;
		}

		public void Defending() => _canDefense = true;
		public void Undefending() => _canDefense = false;

		private void Update()
		{
			EnableShield();
		}

		public void EnableShield()
		{
			if (_playerSettings.Mana > 0 && _canDefense)
			{
				_fireShield.gameObject.SetActive(true);
				SetDamage?.Invoke(_canDefense);
				ReduceDefenceMana();
			}
		}

		private void ReduceDefenceMana()
		{
			_playerSettings.Mana -= _playerSettings.ShieldAmount * Time.deltaTime;

			if (_playerSettings.Mana <= 0)
			{
				_playerSettings.Mana = 0;
			}
		}

		public void DisableShield()
		{
			_fireShield.gameObject.SetActive(false);
			SetDamage?.Invoke(_canDefense);
		}
	}

}
