using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.FSM
{
	public class BaseStateMachine : MonoBehaviour
	{
		[SerializeField] private string _state;
		[SerializeField] private BaseState _initalState;
		[SerializeField] Dictionary<Type, Component> _cachedComponents;

		public BaseState CurrentState { get; set; }

		private void Awake()
		{
			_cachedComponents = new Dictionary<Type, Component>();
			CurrentState = _initalState;
		}

		private void Update()
		{
			CurrentState.Execute(this);
			_state = CurrentState.ToString();
		}

		public new T GetComponent<T>() where T : Component
		{
			if (_cachedComponents.ContainsKey(typeof(T)))
				return _cachedComponents[typeof(T)] as T;

			var component = base.GetComponent<T>();
			if (component != null)
			{
				_cachedComponents.Add(typeof(T), component);
			}
			Debug.Log(component);
			return component;
		}
	}
}

