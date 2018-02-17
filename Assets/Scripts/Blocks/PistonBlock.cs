using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonBlock : Block
{
	public Transform piston;
	bool extended = false;

	private void Awake()
	{
		piston = transform.Find("Piston");
	}

	public override void Activate(float time)
	{
		if (!extended)
		{
			piston.transform.position = (piston.transform.up * time) + transform.position;
			if (sides[0].adjacentSide != null)
				sides[0].adjacentSide.transform.root.GetComponent<Block>().Move(transform.up, time);
			if (time == 1)
				extended = true;
		}
	}
	public override void Deactivate(float time)
	{
		if (extended)
		{
			piston.transform.position = (piston.transform.up * (1 - time)) + transform.position;
			if (time == 1)
				extended = false;
		}
	}

}
