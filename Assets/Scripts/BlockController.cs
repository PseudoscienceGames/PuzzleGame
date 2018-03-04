﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
	public List<Block> blocks = new List<Block>();
	public List<Block> blocksToActivate = new List<Block>();
	public List<Block> blocksToFall = new List<Block>();
	public float time;
	public float tickSpeed;

	public static BlockController Instance;
	private void Awake(){ Instance = this; }

	public void Initialize()
	{
		foreach(Block b in GameObject.FindObjectsOfType<Block>())
		{
			blocks.Add(b);
		}
		StartCoroutine(Tick());
	}

	public void DeleteBlock(Block b)
	{
		blocks.Remove(b);
		DestroyImmediate(b.gameObject);
	}

	IEnumerator Tick()
	{
		List<Block> blocksToCheck = new List<Block>();
		List<Block> checkedBlocks = new List<Block>();
		blocksToActivate.Clear();
		blocksToFall.Clear();
		foreach (Block b in blocks)
		{
			Vector3 pos = b.transform.position;
			b.loc = new Vector3Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z));
			b.transform.position = b.loc;
		}
		foreach (Block b in blocks)
		{
			b.CheckSides();
			if (b.GetComponent<PowerSourceBlock>() != null)
				blocksToCheck.Add(b);
			if (b.DirToSide(-Vector3.up).adjacentSide == null && b.loc.y > 0)
				blocksToFall.Add(b);
		}
		while(blocksToCheck.Count > 0)
		{
			Block b = blocksToCheck[0];
			foreach (Side s in b.sides)
			{
				if(s.adjacentSide != null && s.type == s.adjacentSide.type && s.type != SideType.Base && !checkedBlocks.Contains(s.adjacentSide.transform.root.GetComponent<Block>()))
				{
					if(s.type != SideType.Gear || s.transform.root.up == s.adjacentSide.transform.root.up || s.transform.root.up == -s.adjacentSide.transform.root.up)
						blocksToCheck.Add(s.adjacentSide.transform.root.GetComponent<Block>());
					if (s.type == SideType.Gear)
					{
						if (s.transform.root.up == -s.adjacentSide.transform.root.up)
							s.adjacentSide.transform.root.GetComponent<Block>().dir = s.transform.root.GetComponent<Block>().dir;
						else
							s.adjacentSide.transform.root.GetComponent<Block>().dir = -s.transform.root.GetComponent<Block>().dir;
					}
						if (s.type == SideType.Axle)
					{
						if(s.transform.root.rotation == s.adjacentSide.transform.root.rotation)
							s.adjacentSide.transform.root.GetComponent<Block>().dir = s.transform.root.GetComponent<Block>().dir;
						else
							s.adjacentSide.transform.root.GetComponent<Block>().dir = -s.transform.root.GetComponent<Block>().dir;
					}
				}
			}
			blocksToActivate.Add(b);
			checkedBlocks.Add(b);
			blocksToCheck.RemoveAt(0);
		}

		time = 0;
		while(time < 1)
		{
			time += Time.deltaTime * tickSpeed;
			if (time > 1)
				time = 1;
			foreach(Block b in blocks)
			{
				if (blocksToActivate.Contains(b))
					b.Activate(time);
				else
					b.Deactivate(time);
				if (blocksToFall.Contains(b))
					b.Move(Vector3.down, time);
			}
			yield return null;
		}
		yield return Tick();
	}
}
