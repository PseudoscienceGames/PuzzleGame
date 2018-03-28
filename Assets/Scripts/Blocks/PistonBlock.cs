using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonBlock : ManipulatorBlock
{
	public GameObject pistonBlock;
	public List<MovableBlock> grabbedBlocks = new List<MovableBlock>();
	public bool extended = false;

	public Renderer coloredBit;

	private void Awake()
	{
		coloredBit.material = BlockController.Instance.colorMats[color];
		moveyBit = transform.Find("Piston");
	}

	public override bool TickStart()
	{
		grabbedBlocks.Clear();
		if (extended)
			return true;
		else if (BlockController.Instance.blocks.ContainsKey(VectorToInt(loc + transform.up)) && 
			(color == 0 || BlockController.Instance.blocks[VectorToInt(loc + transform.up)].color == color) &&
			BlockController.Instance.blocks[VectorToInt(loc + transform.up)].GetComponent<MovableBlock>() != null)
		{
			bool keepChecking = true;
			int length = 1;
			while (keepChecking)
			{
				Block b;
				if (BlockController.Instance.blocks.ContainsKey(VectorToInt(loc + (transform.up * length))))
				{
					b = BlockController.Instance.blocks[VectorToInt(loc + (transform.up * length))];
					if (b.GetComponent<MovableBlock>() == null || b.GetComponent<MovableBlock>().grabbed)
						return false;
					else
						grabbedBlocks.Add(b.GetComponent<MovableBlock>());
					length++;
				}
				else
				{
					foreach (MovableBlock b2 in grabbedBlocks)
						b2.grabbed = true;
					keepChecking = false;
					return true;
				}
			}
		}
		return false;
	}

	public override void Tick(float time)
	{
		if (!extended)
		{
			moveyBit.localPosition = new Vector3(0, time, 0);
			foreach(MovableBlock b in grabbedBlocks)
				b.Move(transform.up, time);
		}
		else
		{
			moveyBit.localPosition = new Vector3(0, (1f - time), 0);
		}
	}

	public override void TickEnd()
	{
		base.TickEnd();
		foreach(MovableBlock b in grabbedBlocks)
		{
			b.grabbed = false;
		}
		grabbedBlocks.Clear();
		if (!extended)
		{
			pistonBlock.SetActive(true);
			extended = true;
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
