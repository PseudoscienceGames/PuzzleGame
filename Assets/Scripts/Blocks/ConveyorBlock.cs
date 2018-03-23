using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBlock : Block
{
	private void Awake()
	{
		moveyBit = transform.Find("Belt");
	}

	public override bool TickStart()
	{
		if (!grabbed && BlockController.Instance.blocks.ContainsKey(VectorToInt(loc + transform.up)) &&
			!BlockController.Instance.blocks.ContainsKey(VectorToInt(loc + transform.up + transform.forward)) &&
			(color == 0 || BlockController.Instance.blocks[VectorToInt(loc + transform.up)].color == color))
		{
			grabbedBlock = BlockController.Instance.blocks[VectorToInt(loc + transform.up)].transform;
			grabbedBlock.GetComponent<Block>().grabbed = true;
			return true;
		}
		else
			return false;
	}

	public override void Tick(float time)
	{
		grabbedBlock.GetComponent<Block>().Move(transform.forward, time, false);
	}

	public override void TickEnd()
	{
		base.TickEnd();
		grabbedBlock.GetComponent<Block>().grabbed = false;
		grabbedBlock = null;
	}
}
