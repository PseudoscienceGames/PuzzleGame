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
		List<Vector3Int> toCheck = new List<Vector3Int>();
		toCheck.Add(loc + Vector3Int.up + new Vector3Int(0, 0, 1));
		toCheck.Add(loc + Vector3Int.up);
		toCheck.Add(loc + Vector3Int.up + new Vector3Int(0, 0, -1));
		foreach (Vector3Int v in toCheck)
		{
			if (BlockController.Instance.blocks.ContainsKey(v))
				BlockController.Instance.CollisionWarning(BlockController.Instance.blocks[v]);
		}
		if (!flipped)
		{
			if (grabbed == null && sides[5].adjacentSide != null)
			{
				grabbed = sides[5].adjacentSide.transform.root;
				grabbed.parent = arm;
			}
			arm.transform.localEulerAngles = new Vector3(180f * time, 0, 0);

			if (time == 1)
			{
				flipped = true;
				if (grabbed != null)
				{
					Debug.Log("deparent");
					grabbed.parent = null;
					grabbed.root.localScale = Vector3.one;
					grabbed = null;
				}
			}
		}
	}
	public override void Deactivate(float time)
	{
		if(flipped)
			arm.transform.localEulerAngles = new Vector3(180f * (1f - time), 0, 0);

		if (time == 1)
		{
			arm.transform.localEulerAngles = Vector3.zero;
			flipped = false;
			if (grabbed != null)
			{
				grabbed.parent = null;
				BlockController.Instance.ResetBlock(grabbed.GetComponent<Block>());
				grabbed = null;
			}
		}
	}
}
