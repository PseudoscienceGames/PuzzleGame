using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireBlock : Block
{
	private Dictionary<GridLoc, GameObject> connections = new Dictionary<GridLoc, GameObject>();
	private Dictionary<GridLoc, GameObject> cups = new Dictionary<GridLoc, GameObject>();

	public override void Start()
	{
		connections.Add(GridLoc.up, transform.Find("Up").gameObject);
		connections.Add(GridLoc.down, transform.Find("Down").gameObject);
		connections.Add(GridLoc.right, transform.Find("Right").gameObject);
		connections.Add(GridLoc.left, transform.Find("Left").gameObject);
		connections.Add(GridLoc.forward, transform.Find("Forward").gameObject);
		connections.Add(GridLoc.back, transform.Find("Back").gameObject);
		cups.Add(GridLoc.up, transform.Find("UpCup").gameObject);
		cups.Add(GridLoc.down, transform.Find("DownCup").gameObject);
		cups.Add(GridLoc.right, transform.Find("RightCup").gameObject);
		cups.Add(GridLoc.left, transform.Find("LeftCup").gameObject);
		cups.Add(GridLoc.forward, transform.Find("ForwardCup").gameObject);
		cups.Add(GridLoc.back, transform.Find("BackCup").gameObject);
		base.Start();
		transfersPower = true;
	}

	public override void EndLastTick()
	{
		base.EndLastTick();
		foreach (GridLoc g in connections.Keys)
		{
			
			GridLoc toCheck = gridLoc + g;
			Debug.Log(name + " " + g + " " + GridController.instance.grid.ContainsKey(toCheck));
			if (GridController.instance.grid.ContainsKey(toCheck))
			{
				Block b = GridController.instance.grid[toCheck];
				if (b.takesPower || b.transfersPower || b.GetComponent<PowerSourceBlock>() != null)
					connections[g].SetActive(true);
				else
					connections[g].SetActive(false);
				if (b.takesPower || b.GetComponent<PowerSourceBlock>() != null)
					cups[g].SetActive(true);
				else
					cups[g].SetActive(false);
			}
			else
			{
				connections[g].SetActive(false);
				cups[g].SetActive(false);
			}
		}
	}
}
