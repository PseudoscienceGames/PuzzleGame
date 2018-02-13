using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildState : State
{
	public override void Activate()
	{
		
	}

	public void Play()
	{
		GameObject.Find("Cursor").SetActive(false);
		GameObject.Find("BuildUI").SetActive(false);
		GetComponent<StateMachine>().ChangeState<PlayState>();
	}
}
