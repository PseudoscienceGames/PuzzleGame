using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	public GridLoc gridLoc;
	public bool locked = false;
	public bool takesPower = false;

	public virtual void Activate()
	{
		MyGrid.instance.blocksChecked.Add(this);
		Debug.Log(name);
	}
	public GridLoc SetGridLoc()
	{
		gridLoc = new GridLoc(transform.position);
		return gridLoc;
	}
	public virtual void EndTick()
	{

	}
}