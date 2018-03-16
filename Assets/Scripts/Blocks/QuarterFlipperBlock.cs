using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuarterFlipperBlock : Block
{
	public Transform arm;
	public Transform grabbedBlock;
	public bool flipped = false;

	private void Awake()
	{
		arm = transform.Find("Arm");
	}

	public override bool CheckToActivate()
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

	public override void Activate(float time)
	{
		if (!flipped)
		{
			grabbedBlock.parent = arm;
			arm.localEulerAngles = new Vector3(90f * time, 0, 0);

			if (time == 1)
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

		}
		else
		{
			arm.localEulerAngles = new Vector3(90f * (1 - time), 0, 0);
			if (time == 1)
			{
				arm.transform.localPosition = Vector3.zero;
				flipped = false;
			}
		}
	}

	public override void Reset()
	{
		base.Reset();
		grabbedBlock = null;
		arm.localEulerAngles = Vector3.zero;
		flipped = false;
	}
}
