using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Side
{
	Block b;
	GridLoc localSide;
	public SideType sType;

	public Side(SideType s, Block p, GridLoc g)
	{
		sType = s;
		b = p;
		localSide = g;
	}

	public Side FindAdjacentSide()
	{
		Side s;
		GridLoc g = b.gridLoc + localSide;
		if (GridController.instance.grid.ContainsKey(g))
		{
			s = GridController.instance.grid[g].FindWorldSide(-localSide);
			return s;
		}
		else
			return null;
	}
}

public enum SideType { Base, Gear, Axle, Power, S1, S2, S3, S4, S5}