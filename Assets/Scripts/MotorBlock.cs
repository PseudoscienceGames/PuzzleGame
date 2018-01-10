using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorBlock : Block
{
	private Transform axle;
	bool rot = false;

	public void Start()
	{
		takesPower = true;
		axle = transform.Find("Axle");
	}

	public override void Activate()
	{
		base.Activate();
		rot = true;
		GridLoc g = gridLoc + new GridLoc(transform.up);
		if (MyGrid.instance.blocks.ContainsKey(g))
		{
			Block b = MyGrid.instance.blocks[g];
			if (!MyGrid.instance.blocksChecked.Contains(b) && (b.transform.rotation == transform.rotation || Quaternion.Inverse(b.transform.rotation) == transform.rotation))
			{
				b.Activate();
			}
		}
	}

	private void Update()
	{
		if(rot)
		{
			axle.Rotate(Vector3.up, (90f * Time.deltaTime) / MyGrid.tickTime);
		}
	}
	public override void EndTick()
	{
		base.EndTick();
		rot = false;
	}

}
