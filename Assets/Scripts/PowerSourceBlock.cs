using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSourceBlock : Block
{
	public override void Activate()
	{
		base.Activate();
		foreach (GridLoc g in gridLoc.AdjacentBlocks())
		{
			if (MyGrid.instance.blocks.ContainsKey(g))
			{
				Block b = MyGrid.instance.blocks[g];
				if (b.takesPower && !MyGrid.instance.blocksChecked.Contains(b))
				{
					b.Activate();
				}
			}
		}
	}
}
