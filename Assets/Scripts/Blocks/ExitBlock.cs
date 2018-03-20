﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBlock : Block
{
	public int botsNeeded;
	public int successCount;

	public override bool CheckToActivate()
	{
		if (BlockController.Instance.blocks.ContainsKey(VectorToInt(loc + transform.up)) &&
			(BlockController.Instance.blocks[VectorToInt(loc + transform.up)].color == color || color == 0))
		{
			TakeBot(BlockController.Instance.blocks[VectorToInt(loc + transform.up)]);
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

	void TakeBot(Block b)
	{
		successCount--;
		BlockController.Instance.toDelete.Add(b);
		//
	}
}