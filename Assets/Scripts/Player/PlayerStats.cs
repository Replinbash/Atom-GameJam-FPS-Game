using UnityEngine;

namespace GameJam.Player
{
	public class PlayerStats : CharacterStats
	{
		[SerializeField] private PlayerSettings _playerSettings;

		private void Start()
		{
			InitVariables();
		}

		public override void InitVariables()
		{
			base.InitVariables();
			maxHealth = _playerSettings.MaxHealth;
			SetHealthTo(maxHealth);		
		}
	}
}
