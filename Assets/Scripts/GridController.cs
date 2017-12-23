using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
	public Dictionary<GridLoc, Block> grid = new Dictionary<GridLoc, Block>();
	public Dictionary<GridLoc, Block> newGrid = new Dictionary<GridLoc, Block>();
	public static GridController GC;
	private void Awake(){GC = this;}
	public bool play;
	public float tickTime = 0.25f;

	//make activated by ui button
	private void Start()
	{
		List<Block> blocks = new List<Block>(GameObject.FindObjectsOfType<Block>());
		foreach(Block b in blocks)
		{
			Debug.Log(b.name);
			grid.Add(b.gridLoc, b);
		}
		StartCoroutine("Tick");
	}

	IEnumerator Tick()
	{
		while (play)
		{
			FinishLastTick();
			CheckPowerGrid();
			Actions();
			yield return new WaitForSeconds(tickTime);
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
