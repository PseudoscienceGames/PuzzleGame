using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorBlock : Block
{
	public bool hasPower;
	public Transform Axle;

	public override void AddSides()
	{
		sides.Add(new GridLoc(Vector3.up), new Side(SideType.Axle, this, new GridLoc(Vector3.up)));
		sides.Add(new GridLoc(-Vector3.up), new Side(SideType.S1, this, new GridLoc(-Vector3.up)));
		sides.Add(new GridLoc(Vector3.right), new Side(SideType.S2, this, new GridLoc(Vector3.right)));
		sides.Add(new GridLoc(-Vector3.right), new Side(SideType.S3, this, new GridLoc(-Vector3.right)));
		sides.Add(new GridLoc(Vector3.forward), new Side(SideType.S4, this, new GridLoc(Vector3.forward)));
		sides.Add(new GridLoc(-Vector3.forward), new Side(SideType.S5, this, new GridLoc(-Vector3.forward)));
	}

	public override void Action()
	{
		foreach (Side s in sides.Values)
		{
			if (s.FindAdjacentSide() != null)
				Debug.Log(name + " " + s.sType + " " + s.FindAdjacentSide().sType);
		}
		if(hasPower)
		{
			Axle.Rotate(Vector3.up, Time.deltaTime * 90f);
		}
	}

}
