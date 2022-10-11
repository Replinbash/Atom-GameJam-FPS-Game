using UnityEngine;

namespace GameJam.PlayerCombat
{
	public class ChargeSkill : BaseSkill
	{
		[SerializeField] protected PlayerSettings _playerSettings;

		protected WaitForSeconds _coroutineRange;
		protected int _chargeRate;

		private void Start()
		{
			_playerSettings.Mana = _playerSettings.MaxMana;
			_playerSettings.Stamina = _playerSettings.MaxStamina;

			// coroutine settings
			_chargeRate = 5;
			_coroutineRange = new WaitForSeconds(0.6f);
		}		
	}
}
