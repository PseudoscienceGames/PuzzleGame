using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	public GridLoc gridLoc;
	public Dictionary<GridLoc, Side> sides = new Dictionary<GridLoc, Side>();
	public bool ss = false;
	public bool active = false;

	public virtual void Start()
	{
		gridLoc = new GridLoc(transform.position);
		AddSides();
		GridController.instance.grid.Add(gridLoc, this);
	}
	public virtual void AddSides()
	{
		sides.Add(GridLoc.up, new Side(SideType.Base, this, GridLoc.up));
		sides.Add(GridLoc.down, new Side(SideType.Base, this, GridLoc.down));
		sides.Add(GridLoc.right, new Side(SideType.Base, this, GridLoc.right));
		sides.Add(GridLoc.left, new Side(SideType.Base, this, GridLoc.left));
		sides.Add(GridLoc.forward, new Side(SideType.Base, this, GridLoc.forward));
		sides.Add(GridLoc.back, new Side(SideType.Base, this, GridLoc.back));
		foreach(GridLoc g in sides.Keys)
		{
			Debug.Log(g + " " + FindLocalSideGridLoc(g));
		}
	}
	public virtual void Action(GridLoc g)
	{
		active = true;
	}

	public GridLoc FindWorldSideGridLoc(GridLoc localSide)
	{
		GridLoc worldSide = new GridLoc(transform.rotation * localSide.ToVector3());
		return worldSide;
	}
	public GridLoc FindLocalSideGridLoc(GridLoc worldSide)
	{
		GridLoc localSide = new GridLoc(Quaternion.Inverse(transform.rotation) * worldSide.ToVector3());
		return localSide;
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