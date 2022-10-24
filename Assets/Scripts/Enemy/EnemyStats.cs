using UnityEngine;
using GameJam.EnemyCombat;
using UnityEngine.AI;

namespace GameJam.EnemyCore
{
    public class EnemyStats : CharacterStats
    {
        [SerializeField] private EnemyBaseAttackSO _enemySettings;
        private int _initialDamage;

		private void OnEnable()
		{
			PlayerCombat.DefenceSkill.DefenceActivatedEvent += AbsorveDamage;
		}

		private void OnDisable()
		{
			PlayerCombat.DefenceSkill.DefenceActivatedEvent -= AbsorveDamage;
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
            GetComponent<NavMeshAgent>().isStopped = true;
			GetComponent<CapsuleCollider>().enabled = false;
			Destroy(gameObject, 3);
        }       

        private void AbsorveDamage(bool isDefense)
        {
            _enemySettings.Damage = isDefense ? _enemySettings.Damage = 0 : _enemySettings.Damage = _initialDamage;            
        }
    }
}