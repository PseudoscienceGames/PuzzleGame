using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
	public Dictionary<GridLoc, Block> grid = new Dictionary<GridLoc, Block>();
	public Dictionary<GridLoc, Block> newGrid = new Dictionary<GridLoc, Block>();
	//public Dictionary<Block, GridLoc> moves = new Dictionary<Block, GridLoc>();
	public static GridController GC;
	private void Awake(){GC = this;}
	public bool play;

	private void Start()
	{
		List<Block> blocks = new List<Block>(GameObject.FindObjectsOfType<Block>());
		foreach(Block b in blocks)
		{
			grid.Add(b.gridLoc, b);
		}
		StartCoroutine("Tick");
	}

	IEnumerator Tick()
	{
		while (play)
		{
			string locs = "";
			foreach (GridLoc g in grid.Keys)
			{
				locs += g.ToString();
			}
			Debug.Log("TICK" + locs);
			FinishLastTick();
			CheckPowerGrid();
			Actions();
			yield return new WaitForSeconds(1);
		}
	}
	void FinishLastTick()
	{ 
		newGrid.Clear();
		foreach(Block b in grid.Values)
		{
			b.EndMove();
		}

		grid = new Dictionary<GridLoc, Block>(newGrid);
	}
	void CheckPowerGrid()
	{

	}
	void Actions()
	{
		List<Block> bs = new List<Block>(grid.Values);
		foreach(Block b in bs)
		{
			b.Action();
		}
	}
}
