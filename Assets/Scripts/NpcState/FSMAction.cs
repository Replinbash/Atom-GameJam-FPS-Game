using UnityEngine;

namespace GameJam.FSM
{
	public abstract class FSMAction : ScriptableObject
	{
		public abstract void Execute(BaseStateMachine machine);
	}
}


