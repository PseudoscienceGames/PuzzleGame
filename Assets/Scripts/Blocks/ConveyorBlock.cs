using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBlock : Block
{
	public override void Activate(float time)
	{
		//if (sides[0].adjacentSide != null)
		//	sides[0].adjacentSide.transform.root.GetComponent<Block>().Move(transform.forward, time);
	}
}
