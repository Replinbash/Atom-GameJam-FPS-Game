using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.Core;

namespace GameJam.Enemies
{
    public class MeleeAttack : EnemyBaseAttack
    {
        protected override void Start()
        {
            base.Start();
            CombatBehaviour += AttackSequence;
        }

        protected override void AttackSequence()
        {
            //player durursa
            if (!_hasStopped)
            {
                _hasStopped = true;
                _timeOfLastAttack = Time.time - 1.5f;
            }

            else if (Time.time >= _timeOfLastAttack + _enemySettings.AttackSpeed)
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
