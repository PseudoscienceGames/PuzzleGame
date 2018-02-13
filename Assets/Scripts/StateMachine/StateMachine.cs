using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
	public static StateMachine Instance;

	public State state;

	public void ChangeState<T> () where T : State
	{

		if(state != null)
			state.enabled = false;
		T s = GetComponent<T>();
		if (s == null)
			s = gameObject.AddComponent<T>();
		else
			s.enabled = true;
		state = s;
		state.Activate();
	}

	private void Awake()
	{
		Instance = this;
		ChangeState<LoadMainMenuState>();
	}
}
