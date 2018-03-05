using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperBlock : Block
{
	public Transform arm;
	public bool flipped = false;
	public Transform grabbed;

	private void Awake()
	{
		arm = transform.Find("Arm");
	}

	public override void Activate(float time)
	{
		if (!flipped)
		{
			if (grabbed == null && sides[5].adjacentSide != null)
			{
				grabbed = sides[5].adjacentSide.transform.root;
				grabbed.parent = arm;
			}
			arm.transform.eulerAngles = new Vector3(180f * time, 0, 0);

			if (time == 1)
			{
				flipped = true;
				if (grabbed != null)
				{
					Debug.Log("deparent");
					grabbed.parent = null;
				}
			}
		}
	}
	public override void Deactivate(float time)
	{
		if (flipped)
		{
			arm.transform.eulerAngles = new Vector3(180f * (1f - time), 0, 0);

			if (time == 1)
			{
				flipped = false;
			}
		}
	}
}
