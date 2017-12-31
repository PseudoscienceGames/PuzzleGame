using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Side
{
	public Block b;
	public GridLoc localSide;
	public SideType sType;

	public Side(SideType s, Block p, GridLoc g)
	{
		sType = s;
		b = p;
		localSide = g;
	}

	public Side FindAdjacentSide()
	{
		GridLoc g = b.gridLoc + b.FindWorldSideGridLoc(localSide);
		if (GridController.instance.grid.ContainsKey(g))
		{
			Block nB = GridController.instance.grid[g];
			Debug.Log(g + " " + nB.FindLocalSideGridLoc(g));
			Side s = nB.sides[nB.FindLocalSideGridLoc(g)];
			return s;
		}
		else
			return null;
	}
}

public enum SideType { Base, Gear, Axle, Power, S1, S2, S3, S4, S5}