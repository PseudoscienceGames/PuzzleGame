using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBlock : MovableBlock
{
	private void Awake()
	{
		coloredBit.material = BlockController.Instance.colorMats[color];
	}

	public override void Tick(float time)
	{
		if (!grabbed)
		{
			if (!BlockController.Instance.blocks.ContainsKey(VectorToInt(loc - Vector3.up)) && loc.y > 0)
				Move(-Vector3.up, time);
			else if (BlockController.Instance.blocks.ContainsKey(VectorToInt(loc - transform.up)) || VectorToInt(loc - transform.up).y < 0)
			{
				if (!BlockController.Instance.blocks.ContainsKey(VectorToInt(loc + transform.forward)))
					Move(transform.forward, time);
			}
		}
	}
}