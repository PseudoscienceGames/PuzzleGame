using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBlock : Block
{
	public int botsNeeded;
	public int successCount;
	public Renderer coloredBit;

	private void Awake()
	{
		coloredBit.material = BlockController.Instance.colorMats[color];
	}

	public override bool TickStart()
	{
		if (BlockController.Instance.blocks.ContainsKey(VectorToInt(loc + transform.up)) &&
			(BlockController.Instance.blocks[VectorToInt(loc + transform.up)].color == color || color == 0))
		{
			TakeBlock(BlockController.Instance.blocks[VectorToInt(loc + transform.up)]);
			if (successCount == 0)
				BlockController.Instance.CheckSuccess();
		}
		return false;
	}

	public override void Reset()
	{
		successCount = botsNeeded;
		base.Reset();
	}

	void TakeBlock(Block b)
	{
		successCount--;
		b.gameObject.SetActive(false);
	}
}
