using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorBlock : Block
{
	public override void CheckSides()
	{
		base.CheckSides();
		if (sides[0].adjacentSide != null)
			sides[4].type = SideType.Power;
		else
			sides[4].type = SideType.Base;
	}
}
