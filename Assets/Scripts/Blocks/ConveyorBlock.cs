using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBlock : Block
{
	public Block blockAbove;
	public override bool CheckToActivate()
	{
		if (BlockController.Instance.blocks.ContainsKey(VectorToInt(loc + transform.up)) &&
			!BlockController.Instance.blocks.ContainsKey(VectorToInt(loc + transform.up + transform.forward)) &&
			(color == 0 || BlockController.Instance.blocks[VectorToInt(loc + transform.up)].color == color))
		{
			blockAbove = BlockController.Instance.blocks[VectorToInt(loc + transform.up)];
			blockAbove.grabbed = true;
			return true;
		}
		else
			return false;
	}

	private void Awake()
	{
		moveyBit = transform.Find("Belt");
	}

	public override void Activate(float time)
	{
		blockAbove.Move(transform.forward, time);
		if (time == 1)
		{
			blockAbove.grabbed = false;
			blockAbove = null;
		}
	}
}
