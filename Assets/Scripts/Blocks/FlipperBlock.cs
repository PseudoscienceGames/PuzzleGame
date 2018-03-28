using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperBlock : ManipulatorBlock
{
	public bool flipped = false;
	public float angle;

	private void Awake()
	{
		moveyBit = transform.Find("Arm");
	}

	public override bool TickStart()
	{
		//add a check for blocks that may be collided with
		if (grabbedBlock == null && BlockController.Instance.blocks.ContainsKey(VectorToInt(loc - transform.forward)) &&
			!flipped && (color == 0 || BlockController.Instance.blocks[VectorToInt(loc - transform.forward)].color == color) &&
			BlockController.Instance.blocks[VectorToInt(loc - transform.forward)].GetComponent<MovableBlock>() != null)
		{
			MovableBlock b = BlockController.Instance.blocks[VectorToInt(loc - transform.forward)].GetComponent<MovableBlock>();
			if (!b.grabbed)
			{
				grabbedBlock = b.transform;
				b.grabbed = true;
				if (BlockController.Instance.blocksToActivate.Contains(b))
					BlockController.Instance.blocksToActivate.Remove(b);
				return true;
			}
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
			moveyBit.localEulerAngles = new Vector3(angle * time, 0, 0);
		}
		else
			moveyBit.localEulerAngles = new Vector3(angle * (1 - time), 0, 0);
	}

	public override void TickEnd()
	{
		base.TickEnd();
		if (!flipped)
		{
			flipped = true;
			if (grabbedBlock != null)
			{
				grabbedBlock.parent = BlockController.Instance.transform;
				grabbedBlock.localScale = Vector3.one;
				grabbedBlock.GetComponent<MovableBlock>().grabbed = false;
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
		if (grabbedBlock != null)
		{
			grabbedBlock.parent = BlockController.Instance.transform;
			grabbedBlock.localScale = Vector3.one;
			grabbedBlock.GetComponent<MovableBlock>().grabbed = false;
			grabbedBlock = null;
		}
		moveyBit.localEulerAngles = Vector3.zero;
		flipped = false;
	}
}
