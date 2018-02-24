using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearBlock : Block
{
	public Transform gear;

	public override void Activate(float time)
	{
		gear.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.up * 90 * dir) * transform.rotation, time);
	}
}
