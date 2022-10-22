using System.Collections.Generic;
using UnityEngine;

namespace GameJam.FSM
{
	[CreateAssetMenu(menuName = "FSM/State")]
	public class State : BaseState
	{
		public List<FSMAction> Action = new List<FSMAction>();
		public List<FSMTransition> Transition = new List<FSMTransition>();

		public override void Execute(BaseStateMachine machine)
		{
			foreach (var action in Action)			
				action.Execute(machine);

			foreach (var transition in Transition)
				transition.Execute(machine);			
		}
	}

}

