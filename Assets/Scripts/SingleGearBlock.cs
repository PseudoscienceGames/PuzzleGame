using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleGearBlock : Block
{
	public bool rot;
	private Transform gear;

	public void Start()
	{
		gear = transform.Find("Gear");
	}

	public override void Activate()
	{
		base.Activate();
		rot = true;
	}

	private void Update()
	{
		if (rot)
		{
			gear.Rotate(Vector3.up, (90f * Time.deltaTime) / MyGrid.tickTime);
		}
	}
}
