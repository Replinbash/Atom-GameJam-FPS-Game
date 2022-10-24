using UnityEngine;
using GameJam.EnemyCore;

namespace GameJam.EnemyCombat
{
	public class MeleeAttack : EnemyBaseAttack
	{
		[SerializeField] private EnemyMaleeAttackSO _meleeSettings;
		[SerializeField] private GameObject _attackPoint;
		
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
				_animatonController.animator.SetTrigger(EnemyAnimatonController.ANIMATOR_PARAM_NPC_MELEE_ATTACK);
			}
		}

		public void AttackEvent()
		{
			Collider[] hitResults = Physics.OverlapSphere(_attackPoint.transform.position,
			_meleeSettings.AttackSpeed, _meleeSettings.TargetLayer);

			if (hitResults.Length == 0)
				return;

			else
			{
				_enemyStats.DealDamage(_playerStats);
				Debug.Log("Player take damage by melee enemy!");
			}
		}

		private void OnDrawGizmosSelected()
		{
			if (_attackPoint == null)
				return;
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(_attackPoint.transform.position, _meleeSettings.AttackRange);
		}
	}
}
