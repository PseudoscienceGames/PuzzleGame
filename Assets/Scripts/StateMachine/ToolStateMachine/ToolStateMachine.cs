using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolStateMachine : MonoBehaviour
{
	public static ToolStateMachine Instance;
	public ToolState lastState;

	public ToolState state;

	public void ChangeState<T> () where T : ToolState
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
		ChangeState<SelectionToolState>();
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.X))
		{
			ChangeState<SelectionToolState>();
		}
		if (Input.GetKeyDown(KeyCode.C))
		{
			ChangeState<RotateToolState>();
		}
		if (Input.GetKeyDown(KeyCode.V))
		{
			ChangeState<TranslateToolState>();
		}
	}
}
