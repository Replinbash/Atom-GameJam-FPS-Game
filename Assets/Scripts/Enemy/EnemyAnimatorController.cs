using GameJam.FSM;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GameJam.EnemyCore
{
	public class EnemyAnimatorController : MonoBehaviour
	{
		private Animator _animator;
		private NavMeshAgent _navMeshAgent;		

		private static int ANIMATOR_PARAM_NPC_SPEED = Animator.StringToHash("Speed");

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
			_animator = GetComponent<Animator>();
			_navMeshAgent = GetComponent<NavMeshAgent>();	
		}

		public void PlayAnim()
		{
			_animator.SetFloat(ANIMATOR_PARAM_NPC_SPEED, _navMeshAgent.velocity.magnitude);
			Debug.Log("Play Anim");
		}

	}

}
