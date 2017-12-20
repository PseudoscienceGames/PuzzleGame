using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
	public Dictionary<GridLoc, Block> grid = new Dictionary<GridLoc, Block>();
	public Dictionary<Block, GridLoc> moves = new Dictionary<Block, GridLoc>();
	public static GridController GC;
	private void Awake(){GC = this;}

	private void Start()
	{
		List<Block> blocks = new List<Block>(GameObject.FindObjectsOfType<Block>());
		foreach(Block b in blocks)
		{
			grid.Add(b.gridLoc, b);
		}
	}

	private void FixedUpdate()
	{
		CheckPowerGrid();
		Actions();
		Moves();
	}
	void CheckPowerGrid()
	{

	}
	void Actions()
	{
		moves.Clear();
		List<Block> bs = new List<Block>(grid.Values);
		foreach(Block b in bs)
		{
			b.Action();
		}
	}
	void Moves()
	{
		foreach(Block b in moves.Keys)
		{
			b.Move(moves[b]);
		}
	}
}
