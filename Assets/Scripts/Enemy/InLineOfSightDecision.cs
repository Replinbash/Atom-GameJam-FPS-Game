using Gamejam.FSM;
using GameJam.FSM;
using UnityEngine;

namespace GameJam.EnemyCore
{
	[CreateAssetMenu(menuName ="FSM/Decisions/In Line Of Sight")]
	public class InLineOfSightDecision : Decision
	{
		public override bool Decide(BaseStateMachine stateMachine)
		{
			var enemySightSensor = stateMachine.GetComponent<EnemySightSensor>();
			return enemySightSensor.Ping();
		}
	}
}

