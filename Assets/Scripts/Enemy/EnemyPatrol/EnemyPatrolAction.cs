using UnityEngine;
using GameJam.FSM;
using UnityEngine.AI;

namespace GameJam.EnemyPatrol
{
	[CreateAssetMenu(menuName = "FSM/Actions/Patrol")]
	public class EnemyPatrolAction : FSMAction
	{
		public override void Execute(BaseStateMachine stateMachine)
		{
			var navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
			var patrolPoints = stateMachine.GetComponent<PatrolPoints>();

			if (patrolPoints.HasReached(navMeshAgent))
			{
				navMeshAgent.SetDestination(patrolPoints.GetNext().position);
			}
				
		}
	}

}

