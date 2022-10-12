using UnityEngine;

namespace GameJam.PlayerCombat
{
	public class ChargeSkill : BaseSkill
	{
		[SerializeField] protected PlayerSettings _playerSettings;

		protected int _chargeRate;
		protected WaitForSeconds _coroutineRange;
		protected Coroutine _reloadStamina = null;
		protected Coroutine _reloadMana = null;


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
