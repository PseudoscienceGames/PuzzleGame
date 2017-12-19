using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BlockController : MonoBehaviour
{
	public Dictionary<GridLoc, Block> blocks = new Dictionary<GridLoc, Block>();
	public float tickTime = 1f;
	public List<Block> powerSources = new List<Block>();

	public bool start;

	private void Update()
	{
		if(start)
		{
			start = false;
			Begin();
		}
	}

	public void Begin ()
	{
		List<Block> bs = GameObject.FindObjectsOfType<Block>().ToList<Block>();
		foreach (Block b in bs)
		{
			b.powered = false;
			GridLoc g = new GridLoc(b.transform.position);
			blocks.Add(g, b);
		}
		InvokeRepeating("Tick", 0, tickTime);
	}

	void Tick()
	{
		EndLastMove();
		powerSources.Clear();
		string gs = "";
		foreach (Block b in blocks.Values)
		{
			gs += b.gridLoc;
			b.powered = false;
			if (b.powerSource)
				powerSources.Add(b);
		}
		Debug.Log(gs);
		PowerGrid();
		Act();

	}

	void EndLastMove()
	{
		foreach(Block b in blocks.Values)
		{
			if (b.moving)
				b.EndMove();
		}
	}

	void PowerGrid()
	{
		List<Block> toCheck = new List<Block>(powerSources);
		List<Block> done = new List<Block>();
		while(toCheck.Count > 0)
		{
			Block b = toCheck[0];
			foreach(GridLoc g in b.gridLoc.AdjacentBlocks())
			{
				if(blocks.ContainsKey(g))
				{
					Block b2 = blocks[g];
					if (!toCheck.Contains(b2) && !done.Contains(b2))
					{
						b2.powered = true;
						if (b2.transfersPower)
							toCheck.Add(b2);
					}
				}
			}
			done.Add(b);
			toCheck.Remove(b);
		}
	}
	void Act()
	{
		foreach(Block b in blocks.Values)
		{
			if (b.powered)
				b.Act();
		}
	}

}
