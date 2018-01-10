using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlock : Block
{
	public GameObject bulb;

	public void Start()
	{
		takesPower = true;
		bulb = transform.Find("Point light").gameObject;
	}
	public override void Activate()
	{
		base.Activate();
		bulb.SetActive(true);
	}
	public override void EndTick()
	{
		base.EndTick();
		bulb.SetActive(false);
	}
}
