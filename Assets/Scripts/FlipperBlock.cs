using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperBlock : Block
{
	private Transform arm;
	private Transform grabbed;
	public int stage = 0;

	public override void Start()
	{
		base.Start();
		arm = transform.Find("Arm");
	}

	public override void Action()
	{
		if (hasPower)
		{
			GridLoc forward = gridLoc + new GridLoc(transform.forward);
			if (GridController.GC.grid.ContainsKey(forward) && stage == 0)
			{//make it so block being flipped takes up any gridLoc its passing through and throws the player and error if any colflicts happen
				Block b = GridController.GC.grid[forward];
				//GridController.GC.grid.Remove(b.gridLoc);
				b.transform.parent = arm;
				grabbed = b.transform;
				stage++;
				StartCoroutine("TickRot");
			}
			else if (stage < 2 && stage != 0)
			{
				stage++;
				StartCoroutine("TickRot");
			}
			else if (stage == 2)
			{
				StartCoroutine("Return");
				stage = 0;
			}
		}
	}
	IEnumerator TickRot()
	{
		Quaternion initRot = arm.transform.rotation;
		Quaternion targetRot = transform.rotation;
		float t = 0;
		targetRot *= Quaternion.Euler(-90 * stage, 0, 0);
		while (t < 1)
		{
			t += Time.deltaTime / GridController.GC.tickTime;
			if (t > 1)
				t = 1;
			arm.rotation = Quaternion.Lerp(initRot, targetRot, t);
			yield return null;
		}
		if(stage == 2)
		{
			grabbed.parent = null;
			grabbed = null;
		}
		yield return null;
	}
	IEnumerator Return()
	{
		Quaternion initRot = arm.transform.rotation;
		Quaternion targetRot = arm.transform.rotation * Quaternion.Euler(-180, 0, 0);
		float t = 0;
		while (t < 1)
		{
			t += Time.deltaTime / GridController.GC.tickTime;
			if (t > 1)
				t = 1;
			arm.rotation = Quaternion.Lerp(initRot, targetRot, t);
			yield return null;
		}
		arm.rotation = transform.rotation;
		yield return null;
	}
}
