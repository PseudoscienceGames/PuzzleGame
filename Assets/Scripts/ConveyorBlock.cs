using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBlock : Block
{
	private void FixedUpdate()
	{
		transform.position += Vector3.forward * Time.fixedDeltaTime;
	}

	public override void Action()
	{
		if (hasPower)
		{
			GridLoc up = gridLoc + new GridLoc(Vector3.up);
			if (GridController.GC.grid.ContainsKey(up))
			{
				Block b = GridController.GC.grid[up];
				GridController.GC.moves.Add(b, new GridLoc(transform.forward));
			}
		}
	}
}
