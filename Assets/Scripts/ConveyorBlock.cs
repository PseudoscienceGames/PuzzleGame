using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBlock : Block
{

	public override void Action()
	{
		if (hasPower)
		{
			
			GridLoc up = gridLoc + new GridLoc(Vector3.up);
			if (GridController.GC.grid.ContainsKey(up))
			{
				Block b = GridController.GC.grid[up];
				b.StartMove(new GridLoc(transform.forward));
			}
		}
	}
}
