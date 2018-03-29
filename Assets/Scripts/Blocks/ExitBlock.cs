using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBlock : ManipulatorBlock
{
	public Renderer coloredBit;

	private void Awake()
	{
		coloredBit.material = BlockController.Instance.colorMats[color];
	}

	public override bool TickStart()
	{
		if (BlockController.Instance.blocks.ContainsKey(VectorToInt(loc + transform.up)) &&
			BlockController.Instance.blocks[VectorToInt(loc + transform.up)].GetComponent<MovableBlock>() != null &&
			(BlockController.Instance.blocks[VectorToInt(loc + transform.up)].color == color || color == 0))
		{
			TakeBlock(BlockController.Instance.blocks[VectorToInt(loc + transform.up)]);
		}
		return false;
	}

	public override void Reset()
	{
		base.Reset();
		if (grabbedBlock != null)
		{
			grabbedBlock.GetComponent<MovableBlock>().grabbed = false;
			grabbedBlock = null;
		}
	}

	void TakeBlock(Block b)
	{
		BlockController.Instance.CheckSuccess();
		grabbedBlock = b.transform;
		b.GetComponent<MovableBlock>().grabbed = true;
	}
}
