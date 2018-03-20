﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonBlock : Block
{
	public Transform grabbedBlock;
	public GameObject pistonBlock;
	public bool extended = false;

	private void Awake()
	{
		moveyBit = transform.Find("Piston");
	}

	public override bool CheckToActivate()
	{
		//add a check for blocks that may be collided with
		if (grabbedBlock == null && BlockController.Instance.blocks.ContainsKey(VectorToInt(loc + transform.up)) &&
			!extended && !BlockController.Instance.blocks.ContainsKey(VectorToInt(loc + (transform.up * 2f))) &&
			(color == 0 || BlockController.Instance.blocks[VectorToInt(loc + transform.up)].color == color))
		{
			Block b = BlockController.Instance.blocks[VectorToInt(loc + transform.up)];
			if (!b.grabbed && !b.locked)
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

	public override void Activate(float time)
	{
		if (!extended)
		{
			moveyBit.localPosition = new Vector3(0, time, 0);
			grabbedBlock.GetComponent<Block>().Move(transform.up, time);

			if (time == 1)
			{
				pistonBlock.SetActive(true);
				extended = true;
				if (grabbedBlock != null)
				{
					grabbedBlock.GetComponent<Block>().grabbed = false;
					grabbedBlock = null;
				}
			}

		}
		else
		{
			moveyBit.localPosition = new Vector3(0, (1f - time), 0);
			if (time == 1)
			{
				pistonBlock.SetActive(false);
				moveyBit.transform.localPosition = Vector3.zero;
				extended = false;
			}
		}
	}

	public override void Reset()
	{
		base.Reset();
		pistonBlock.gameObject.SetActive(false);
		//pistonBlock.transform.position = transform.position;
		grabbedBlock = null;
		moveyBit.localPosition = Vector3.zero;
		extended = false;
	}
	//public override void Deactivate(float time)
	//{
	//	if (extended)
	//	{
	//		piston.transform.position = (piston.transform.up * (1 - time)) + transform.position;
	//		if (time == 1)
	//		{
	//			extended = false;
	//			pistonBlock.SetActive(false);
	//		}
	//	}
	//}

}
