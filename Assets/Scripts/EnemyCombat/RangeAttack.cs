using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.EnemyCombat
{
	public class RangeAttack : EnemyBaseAttack
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

			else if (Time.time >= _timeOfLastAttack + settings.AttackSpeed)
			{
				_timeOfLastAttack = Time.time;
				_canAttack = true;

				if (_animation != null)
				{
					_animation.SetTrigger("rangeAttack");
				}

				Debug.Log("Bowcu Attack Yaptý");
				//StartCoroutine(GetArrow(1));
			}
		}
	}
		
}

