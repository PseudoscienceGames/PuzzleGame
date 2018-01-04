using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridController : MonoBehaviour
{
	public Dictionary<GridLoc, Block> grid = new Dictionary<GridLoc, Block>();
	public List<Block> toAct = new List<Block>();
	public float tickTime = 0.25f;
	public static GridController instance;
	private void Awake(){instance = this;}

	private void Start()
	{
		InvokeRepeating("Tick", 0, tickTime);
	}

	private void Tick()
	{
		EndLastTick();
		CheckPowerGrid();
		PerformBlockActions();
	}

	private void CheckPowerGrid()
	{
		foreach(Block p in grid.Values)
		{
			List<Block> toCheck = new List<Block>();
			List<Block> blocksChecked = new List<Block>();
			blocksChecked.Add(p);
			if(p.GetComponent<PowerSourceBlock>() != null)
			{
				toCheck.Add(p);
				while(toCheck.Count > 0)
				{
					Block b = toCheck[0];
					foreach(GridLoc g in b.gridLoc.AdjacentBlocks())
					{
						//Debug.Log(g);
						if(grid.ContainsKey(g) && !blocksChecked.Contains(grid[g]))
						{
							if (grid[g].takesPower)
							{
								//Debug.Log(grid[g].name);
								toAct.Add(grid[g]);
							}
							if (grid[g].transfersPower)
							{
								//Debug.Log(grid[g].name);
								toCheck.Add(grid[g]);
							}
							blocksChecked.Add(grid[g]);
						}
					}
					toCheck.Remove(b);
				}
			}
		}
	}

	private void PerformBlockActions()
	{
		foreach(Block b in toAct)
		{
			b.Action(GridLoc.zero);
		}
	}

	private void EndLastTick()
	{
		List<Block> bs = grid.Values.ToList<Block>();
		grid.Clear();
		foreach (Block b in bs)
		{
			grid.Add(b.gridLoc, b);
		}
			foreach (Block b in bs)
		{
			b.EndLastTick();
		}
		toAct.Clear();
	}
}
