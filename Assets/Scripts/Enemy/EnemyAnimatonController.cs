using UnityEngine;
using UnityEngine.AI;
using GameJam.FSM;

namespace GameJam.EnemyCore
{
	public class EnemyAnimatonController : MonoBehaviour
	{
		private NavMeshAgent _navMeshAgent;		
		protected internal Animator animator;

		public static int ANIMATOR_PARAM_NPC_SPEED = Animator.StringToHash("Speed");
		public static int ANIMATOR_PARAM_NPC_RANGE_ATTACK = Animator.StringToHash("rangeAttack");
		public static int ANIMATOR_PARAM_NPC_MELEE_ATTACK = Animator.StringToHash("meleeAttack");

		private void OnEnable()
		{
			BaseStateMachine.EnemyEvent += PlayAnim;
		}

		private void OnDisable()
		{
			BaseStateMachine.EnemyEvent -= PlayAnim;
		}

		private void Awake()
		{
			animator = GetComponent<Animator>();
			_navMeshAgent = GetComponent<NavMeshAgent>();	
		}

		public void PlayAnim()
		{
			animator.SetFloat(ANIMATOR_PARAM_NPC_SPEED, _navMeshAgent.velocity.magnitude);
		}

	}

}
