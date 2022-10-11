using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.PlayerCombat
{
	public abstract class BaseSkill : MonoBehaviour
	{
		[SerializeField] protected InputReader _inputReader;

		protected virtual void OnEnable()
		{
			

		}

		protected virtual void OnDisable()
		{
			
		}
	}
}


