using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	public GridLoc gridLoc;
	public List<Side> sides = new List<Side>();

	public virtual void Start()
	{
		gridLoc = new GridLoc(transform.position);
		GridController.instance.grid.Add(gridLoc, this);
	}

	public virtual void Action()
	{

	}
}