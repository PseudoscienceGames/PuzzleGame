using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	public GridLoc gridLoc;
	public Dictionary<GridLoc, Side> sides = new Dictionary<GridLoc, Side>();

	public virtual void Start()
	{
		gridLoc = new GridLoc(transform.position);
		AddSides();
		GridController.instance.grid.Add(gridLoc, this);
	}
	public virtual void AddSides()
	{
		sides.Add(new GridLoc(Vector3.up), new Side(SideType.Base, this, new GridLoc(Vector3.up)));
		sides.Add(new GridLoc(-Vector3.up), new Side(SideType.Base, this, new GridLoc(-Vector3.up)));
		sides.Add(new GridLoc(Vector3.right), new Side(SideType.Base, this, new GridLoc(Vector3.right)));
		sides.Add(new GridLoc(-Vector3.right), new Side(SideType.Base, this, new GridLoc(-Vector3.right)));
		sides.Add(new GridLoc(Vector3.forward), new Side(SideType.Base, this, new GridLoc(Vector3.forward)));
		sides.Add(new GridLoc(-Vector3.forward), new Side(SideType.Base, this, new GridLoc(-Vector3.forward)));
	}
	public virtual void Action()
	{

	}

	public Side FindWorldSide(GridLoc g)
	{
		g = new GridLoc(Quaternion.Inverse(transform.rotation) * g.ToVector3());
		return sides[g];
	}
}