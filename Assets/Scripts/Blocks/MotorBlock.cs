using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorBlock : Block
{
	public Transform axle;

	public override void Activate(float time)
	{
		axle.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.up * 90 * dir) * transform.rotation, time);
	}
}
