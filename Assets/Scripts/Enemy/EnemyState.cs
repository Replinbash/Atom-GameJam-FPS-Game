using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyState 
{
	public enum STATE
	{
		IDLE,
		PATROL,
		CHASE,
		ATTACK
	}

	public enum EVENT
	{
		ENTER,
		UPDATE,
		EXÝT
	}

	public STATE state;
	public EVENT currentEvent;
	public EnemyState nextState;
	public EnemyController enemy;
	public NavMeshAgent agent;
	public Transform player;

	public void State(EnemyController enemy, Transform player, NavMeshAgent agent)
	{
		this.enemy = enemy;
		this.player = player;
		this.agent = agent;
	}

	public EnemyState Process()
	{
		switch (currentEvent)
		{
			case EVENT.ENTER:
				Enter();
				break;

			case EVENT.UPDATE:
				Update();
				break;

			case EVENT.EXÝT:
				Exit();
				break;
		}
		return nextState;
	}

	public virtual void Enter()
	{
		//DO THINGS IN ENTER METHOD
		currentEvent = EVENT.UPDATE;
	}

	public virtual void Update()
	{
		//DO THINGS IN UPDATE METHOD
	}

	public virtual void Exit()
	{
		//DO THINGS IN EXIT METHOD
	}

}
