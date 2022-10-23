using GameJam.FSM;
using UnityEngine;
using UnityEngine.AI;

namespace GameJam.EnemyCombat
{
	[CreateAssetMenu(menuName ="FSM/Actions/Combat")]
	public class EnemyCombatAction : FSMAction
	{
		public override void Execute(BaseStateMachine stateMachine)
		{
			var enemyAttack = stateMachine.GetComponent<EnemyBaseAttack>();
			enemyAttack.StartCoroutine(enemyAttack.AttackProcess());
		}
	}
}
