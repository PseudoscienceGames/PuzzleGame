using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : State
{
	public override void Activate()
	{
		BlockController.Instance.Initialize();
	}
}
