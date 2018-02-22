using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
	public static StateMachine Instance;
	private State lastState;

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
		Debug.Log(state);
	}

	private void Awake()
	{
		Instance = this;
		ChangeState<NoSelectionState>();
	}

	public void ChangeToAddBlockState()
	{
		lastState = state;
		ChangeState<AddBlockState>();
	}

	public void ChangeFromAddBlockState()
	{
		state.enabled = false;
		state = lastState;
		state.enabled = true;
	}

}
