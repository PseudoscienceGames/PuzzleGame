using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearBlock : Block
{
	public Transform gear;

	public override void Activate(float time)
	{
		Vector3 rot = new Vector3(gear.eulerAngles.x, 90f * time * dir, gear.eulerAngles.z);
		gear.eulerAngles = rot;
	}
}
