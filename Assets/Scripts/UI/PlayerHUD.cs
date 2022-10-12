using System.Collections.Generic;
using UnityEngine;
using GameJam.Player;

public class PlayerHUD : MonoBehaviour
{
	[SerializeField] private PlayerSettings _playerSettings;
	[SerializeField] private List<ProgressBar> _bars = new List<ProgressBar>();
	private PlayerStats _playerStats;

	private void Awake()
	{
		_playerStats = GetComponent<PlayerStats>();	
	}

	private void Update()
	{
		_bars[0].UpdateBar(_playerStats.health, _playerSettings.MaxHealth);
		_bars[1].UpdateBar((int)_playerSettings.Stamina, (int)_playerSettings.MaxStamina);
		_bars[2].UpdateBar((int)_playerSettings.Mana, (int)_playerSettings.MaxMana);
	}
}