using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBlock : Block
{
	public int successCount;

	public override bool CheckToActivate()
	{
		if (BlockController.Instance.blocks.ContainsKey(VectorToInt(loc + transform.up)) &&
			BlockController.Instance.blocks[VectorToInt(loc + transform.up)].color == color)
		{
			TakeBot(BlockController.Instance.blocks[VectorToInt(loc + transform.up)]);
			//if (successCount == 0)
			//	BlockController.CheckSuccess();
		}
		return false;
	}

	void TakeBot(Block b)
	{
		successCount--;
		DestroyImmediate(b.gameObject);
		//
	}
}
