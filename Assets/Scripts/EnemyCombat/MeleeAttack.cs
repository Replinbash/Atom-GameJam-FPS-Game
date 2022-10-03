using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.Core;

namespace GameJam.Enemies
{
    public class MeleeAttack : EnemyController
    {
        private void Start()
        {
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

                if (_animation != null) _animation.SetTrigger("meleeAttack");                
                Debug.Log("Melee Attack Yaptý");
            }
        }

        public void AttackEvent()
        {
            Debug.Log("event calisti");
            _enemyStats.DealDamage(_playerStats);
        }
    }
}
