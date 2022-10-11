using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.Player
{
    public class PlayerStats : CharacterStats
    {
        [SerializeField] private PlayerSettings _playerSettings;

        private void Start() => InitVariables();

        public override void InitVariables()
        {
            base.InitVariables();
            SetHealthTo(maxHealth);

            // Player Settings
            health = _playerSettings.Health;
            maxHealth = _playerSettings.MaxHealth;
        }
    }
}
