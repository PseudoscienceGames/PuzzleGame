using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperBlock : Block
{
	public bool flipped = false;

	private void Awake()
	{
		moveyBit = transform.Find("Arm");
	}

	public override bool TickStart()
	{
		//add a check for blocks that may be collided with
		if (grabbedBlock == null && BlockController.Instance.blocks.ContainsKey(VectorToInt(loc - transform.forward)) &&
			!flipped && (color == 0 || BlockController.Instance.blocks[VectorToInt(loc - transform.forward)].color == color))
		{
			Block b = BlockController.Instance.blocks[VectorToInt(loc - transform.forward)];
			if (!b.grabbed)
			{
				grabbedBlock = b.transform;
				b.grabbed = true;
				if (BlockController.Instance.blocksToActivate.Contains(b))
					BlockController.Instance.blocksToActivate.Remove(b);
			}
			return true;
		}
		if (flipped)
			return true;
		return false;
	}

	public override void Tick(float time)
	{
		if (!flipped)
		{
			grabbedBlock.parent = moveyBit;
			moveyBit.localEulerAngles = new Vector3(180f * time, 0, 0);
		}
		else
			moveyBit.localEulerAngles = new Vector3(180f * (1 - time), 0, 0);
	}

	public override void TickEnd()
	{
		if (!flipped)
		{
			flipped = true;
			if (grabbedBlock != null)
			{
				grabbedBlock.parent = null;
				grabbedBlock.localScale = Vector3.one;
				grabbedBlock.GetComponent<Block>().grabbed = false;
				grabbedBlock = null;
			}
		}
		else
		{
			moveyBit.transform.localEulerAngles = Vector3.zero;
			flipped = false;
		}
	}

	public override void Reset()
	{
		base.Reset();
		grabbedBlock = null;
		moveyBit.localEulerAngles = Vector3.zero;
		flipped = false;
	}
}
