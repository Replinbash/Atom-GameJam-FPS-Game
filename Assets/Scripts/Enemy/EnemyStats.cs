using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.Enemies
{
    public class EnemyStats : CharacterStats
    {
        [SerializeField] private EnemyControllerSettings _enemySettings;

        private int _initialDamage;

        private void Start()
        {
            InitVariables();
            PlayerCombat.MagicSkills.SetDamage += FreezeDamage;
        }

        // DealDamage: Hasar vermek.
        public void DealDamage(CharacterStats statsToDamage)
        {
            if (statsToDamage == null)
            {
                return;
            }

            else
            {
                // Take Damage: hasar almak.
                statsToDamage.TakeDamage(_enemySettings.Damage);
            }
        }

        protected override void Die()
        {
            base.Die();
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<EnemyController>().enabled = false;
            Destroy(gameObject, 3);
        }

        public override void InitVariables()
        {
            base.InitVariables();
            maxHealth = _enemySettings.MaxHealth;
            _initialDamage = _enemySettings.Damage;
            SetHealthTo(maxHealth);
        }

        private void FreezeDamage(bool isDefense)
        {
            _enemySettings.Damage = (isDefense) ? _enemySettings.Damage = 0 : _enemySettings.Damage = _initialDamage;
            /*if (isDefense)
            {
                _enemySettings.Damage = 0;
            }

            else
            {
                _enemySettings.Damage = _initialDamage;
            }*/
        }
    }
}