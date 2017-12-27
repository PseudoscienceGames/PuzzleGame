using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
	public Dictionary<GridLoc, Block> grid = new Dictionary<GridLoc, Block>();
	public static GridController instance;
	private void Awake(){instance = this;}

	private void Update()
	{
		foreach(Block b in grid.Values)
		{
			b.Action();
		}
	}
}
