using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorBlock : Block
{
	public bool hasPower;
	public Transform axel;

	public override void Action()
	{
		if(hasPower)
		{
			axel.Rotate(Vector3.up, Time.deltaTime * 90f);
		}
	}

}
