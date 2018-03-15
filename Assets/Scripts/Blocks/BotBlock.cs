using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBlock : Block
{
	private void Awake()
	{
		selfPowered = true;
	}
	public override void Activate(float time)
	{
		if(!moving)
			Move(transform.forward, time);
	}
}