using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonBlock : ManipulatorBlock
{
	public GameObject pistonBlock;
	public List<Block> grabbedBlocks = new List<Block>();
	public bool extended = false;

	private void Awake()
	{
		moveyBit = transform.Find("Piston");
	}

	public override bool TickStart()
	{
		if (grabbedBlock == null && BlockController.Instance.blocks.ContainsKey(VectorToInt(loc + transform.up)) &&
			!extended && (color == 0 || BlockController.Instance.blocks[VectorToInt(loc + transform.up)].color == color) &&
			BlockController.Instance.blocks[VectorToInt(loc + transform.up)].GetComponent<MovableBlock>() != null)
		{
			MovableBlock b = BlockController.Instance.blocks[VectorToInt(loc + transform.up)].GetComponent<MovableBlock>();
			if (!b.grabbed)
			{
				grabbedBlock = b.transform;
				b.grabbed = true;
				if (BlockController.Instance.blocksToActivate.Contains(b))
					BlockController.Instance.blocksToActivate.Remove(b);
				return true;
			}
		}
		if (extended)
			return true;
		return false;
	}

	public override void Tick(float time)
	{
		if (!extended)
		{
			moveyBit.localPosition = new Vector3(0, time, 0);
			grabbedBlock.GetComponent<Block>().Move(transform.up, time);
		}
		else
		{
			moveyBit.localPosition = new Vector3(0, (1f - time), 0);
		}
	}

	public override void TickEnd()
	{
		base.TickEnd();
		if (!extended)
		{
			pistonBlock.SetActive(true);
			extended = true;
			if (grabbedBlock != null)
			{
				grabbedBlock.GetComponent<MovableBlock>().grabbed = false;
				grabbedBlock = null;
			}
		}
		else
		{
			pistonBlock.SetActive(false);
			moveyBit.transform.localPosition = Vector3.zero;
			extended = false;
		}
	}

	public override void Reset()
	{
		base.Reset();
		pistonBlock.gameObject.SetActive(false);
		grabbedBlock = null;
		moveyBit.localPosition = Vector3.zero;
		extended = false;
	}
}
