using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorBlock : Block
{
	public Transform axle;

	public override void Activate(float time)
	{
		Vector3 rot = new Vector3(axle.eulerAngles.x, 90f * time, axle.eulerAngles.z);
		axle.eulerAngles = rot;
	}
}
