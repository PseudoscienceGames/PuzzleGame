using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxleBlock : Block
{
	public Transform axle;

	public override void AddSides()
	{
		base.AddSides();
		sides[GridLoc.up].sType = SideType.Axle;
		sides[GridLoc.down].sType = SideType.Axle;
	}

	public override void Action(GridLoc g)
	{
		base.Action(g);
		Side s = sides[GridLoc.up].FindAdjacentSide();
		if (s != null && s.sType == SideType.Axle && !s.b.active)
			s.b.Action(g);
		s = sides[GridLoc.down].FindAdjacentSide();
		if (s != null && s.sType == SideType.Axle && !s.b.active)
			s.b.Action(g);

		StartCoroutine("Animate", g);
	}

	IEnumerator Animate(GridLoc g)
	{
		float t = 0;
		Quaternion initRot = axle.rotation;
		Quaternion targetRot = Quaternion.Euler(FindWorldSideGridLoc(g).ToVector3() * 90f) * axle.rotation;
		while (t < 1)
		{
			t += Time.deltaTime / GridController.instance.tickTime;
			axle.rotation = Quaternion.Lerp(initRot, targetRot, t);
			yield return null;
		}
		yield return null;
	}

	public override void EndLastTick()
	{
		base.EndLastTick();
		Vector3 rot = axle.eulerAngles;
		rot.x = Mathf.Round(rot.x / 90f) * 90;
		rot.y = Mathf.Round(rot.y / 90f) * 90;
		rot.z = Mathf.Round(rot.z / 90f) * 90;
		axle.eulerAngles = rot;
	}
}
