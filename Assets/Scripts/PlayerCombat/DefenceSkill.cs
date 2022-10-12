using System;
using UnityEngine;

namespace GameJam.PlayerCombat
{
	public class DefenceSkill : BaseSkill
	{
		[SerializeField] private GameObject _fireShield;
		[SerializeField] protected PlayerSettings _playerSettings;
		private bool _canDefense;

		public static event Action<bool> DefenceActivatedEvent;

		protected override void OnEnable()
		{
			base.OnEnable();
			_inputReader.DefenceEvent += Defending;
			_inputReader.AttackEvent += Undefending;
			_inputReader.DefenceCanceledEvent += Undefending;
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			_inputReader.DefenceEvent -= Defending;
			_inputReader.AttackEvent -= Undefending;
			_inputReader.DefenceCanceledEvent -= Undefending;
		}		

		private void Update()
		{
			EnableShield();
			Debug.Log("Can Defense: " + _canDefense);
		}

		public void Defending() => _canDefense = true;
		public void EnableShield()
		{
			if (_playerSettings.Mana > 0 && _canDefense)
			{
				_fireShield.gameObject.SetActive(true);
				DefenceActivatedEvent?.Invoke(_canDefense);
				ReduceDefenceMana();
			}
		}

		private void ReduceDefenceMana()
		{
			_playerSettings.Mana -= _playerSettings.ShieldAmount * Time.deltaTime;

			if (_playerSettings.Mana <= 0)
			{
				_playerSettings.Mana = 0;
				Undefending();
			}
		}

		public void Undefending()
		{
			_canDefense = false;
			_fireShield.gameObject.SetActive(false);
			DefenceActivatedEvent?.Invoke(_canDefense);
		}
	}
}
