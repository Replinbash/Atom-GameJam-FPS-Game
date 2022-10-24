using GameJam.EnemyCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.EnemyCombat
{
	public class RangeAttack : EnemyBaseAttack
	{
		[SerializeField] private ObjectPool _pool;
		[SerializeField] private Transform _arrowSpawnPoint;
		[SerializeField] private GameObject _arrowPrefab;

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

				if (_animatonController.animator != null)
				{
					_animatonController.animator.SetTrigger(EnemyAnimatonController.ANIMATOR_PARAM_NPC_RANGE_ATTACK);
				}

				StartCoroutine(GetArrow(3));
				Debug.Log("Bowcu Attack Yaptý");
			}
		}

		private IEnumerator GetArrow(int timer)
		{
			yield return new WaitForSeconds(timer);	
			GameObject arrow = _pool.GetObject(_arrowPrefab);
			arrow.transform.position = _arrowSpawnPoint.position;
			yield return null;
		}

	}
		
}

