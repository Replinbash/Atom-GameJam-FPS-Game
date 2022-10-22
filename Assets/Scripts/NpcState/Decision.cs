using GameJam.FSM;
using UnityEngine;

namespace Gamejam.FSM
{
	public abstract class Decision : ScriptableObject
	{
		public abstract bool Decide(BaseStateMachine state);
	}

}

