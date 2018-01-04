using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlock : Block
{
	public GameObject bulb;

	public override void Start()
	{
		base.Start();
		takesPower = true;
		bulb = transform.Find("Point light").gameObject;
	}

	public override void EndLastTick()
	{
		base.EndLastTick();
		bulb.SetActive(false);
	}
	public override void Action(GridLoc g)
	{
		base.Action(g);
		bulb.SetActive(true);
	}
}
