using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
	public Dictionary<GridLoc, Block> grid = new Dictionary<GridLoc, Block>();
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
		foreach(Block b in grid.Values)
		{
			b.active = false;
			if(b.ss)
				b.Action(GridLoc.up);
		}
	}

	private void EndLastTick()
	{
		foreach (Block b in grid.Values)
		{
			b.EndLastTick();
		}
	}
}
