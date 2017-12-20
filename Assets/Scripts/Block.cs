using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	public GridLoc gridLoc;
	public bool takesPower;
	public bool givesPower;
	public bool hasPower;

	private void Start()
	{
		gridLoc = new GridLoc(transform.position);
	}

	public void Move(GridLoc dir)
	{
		Debug.Log(name + " " + gridLoc + " " + dir);
		GridController.GC.grid.Remove(gridLoc);
		gridLoc += dir;
		GridController.GC.grid.Add(gridLoc, this);
		transform.position = gridLoc.ToVector3();
	}

	public virtual void Action()
	{

	}
}