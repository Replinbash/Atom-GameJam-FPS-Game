using UnityEngine;

namespace GameJam.EnemyCombat
{
    public class MeleeAttack : EnemyBaseAttack
    {
        protected override void Start()
        {
            base.Start();
        }

        protected override void AttackSequence(EnemyBaseAttackSO settings)
        {
            //player durursa
            if (!_hasStopped)
            {
                _hasStopped = true;
                _timeOfLastAttack = Time.time - 1.5f;
            }

            if (Time.time >= _timeOfLastAttack + settings.AttackSpeed)
            {
                _timeOfLastAttack = Time.time;
                _canAttack = true;

                if (_animation != null)
                {
                    _animation.SetTrigger("meleeAttack");              
                }

                Debug.Log("Melee has attacked!");
            }
        }

        public void AttackEvent()
        {
            Debug.Log("Enemy Melee Attack Event is work!");
            _enemyStats.DealDamage(_playerStats);
        }
    }
}
