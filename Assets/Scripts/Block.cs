using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	public GridLoc gridLoc;
	public bool ss = false;
	public bool active = false;

	public virtual void Start()
	{
		gridLoc = new GridLoc(transform.position);
		GridController.instance.grid.Add(gridLoc, this);
	}
	public virtual void Action(GridLoc g)
	{
		active = true;
	}

	public virtual void EndLastTick()
	{
		StopAllCoroutines();
		transform.position = new GridLoc(transform.position).ToVector3();
		Vector3 rot = transform.eulerAngles;
		rot.x = Mathf.Round(rot.x / 90f) * 90;
		rot.y = Mathf.Round(rot.y / 90f) * 90;
		rot.z = Mathf.Round(rot.z / 90f) * 90;
		transform.eulerAngles = rot;
	}
}