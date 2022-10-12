using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.Enemies
{
    public class EnemyStats : CharacterStats
    {
        [SerializeField] private EnemyControllerSettings _enemySettings;
        private int _initialDamage;

		private void OnEnable()
		{
			PlayerCombat.DefenceSkill.DefenceActivatedEvent += FreezeDamage;
		}

		private void OnDisable()
		{
			PlayerCombat.DefenceSkill.DefenceActivatedEvent -= FreezeDamage;
		}

		private void Start()
        {
            InitVariables();            
        }       

        public override void InitVariables()
		{
			base.InitVariables();
			maxHealth = _enemySettings.MaxHealth;
			_initialDamage = _enemySettings.Damage;
			SetHealthTo(maxHealth);
		}

		public void DealDamage(CharacterStats statsToDamage)
        {
            if (statsToDamage == null)
            {
                return;
            }

            else
            {
                statsToDamage.TakeDamage(_enemySettings.Damage);
            }
        }

        protected override void Die()
        {
            base.Die();
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<EnemyBaseAttack>().enabled = false;
            Destroy(gameObject, 3);
        }       

        private void FreezeDamage(bool isDefense)
        {
            _enemySettings.Damage = isDefense ? _enemySettings.Damage = 0 : _enemySettings.Damage = _initialDamage;            
        }
    }
}