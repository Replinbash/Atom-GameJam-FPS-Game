using GameJam.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
	[SerializeField] private ProgressBar _manaBar, _healthBar, _staminaBar;
	[SerializeField] private PlayerSettings _playerSettings;

	private void Update()
	{
		_staminaBar.SetValues((int)_playerSettings.Stamina, (int)_playerSettings.MaxStamina);
		_manaBar.SetValues((int)_playerSettings.Mana, (int)_playerSettings.MaxMana);
		_healthBar.SetValues(_playerSettings.Health, _playerSettings.MaxHealth);
	}
}