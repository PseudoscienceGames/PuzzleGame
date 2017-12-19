using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : Block
{
	public override void Act()
	{
		base.Act();
		StartMove(new GridLoc(transform.forward));
	}
}
