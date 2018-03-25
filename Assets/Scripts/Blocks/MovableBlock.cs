using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBlock : Block
{
	public bool grabbed;

	public override bool TickStart()
	{
		if (!grabbed)
			return true;
		else
			return false;
	}

	public override void Tick(float time)
	{
		if (!grabbed)
		{
			if (!BlockController.Instance.blocks.ContainsKey(VectorToInt(loc - Vector3.up)) && loc.y > 0)
				Move(-Vector3.up, time);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Col") &&
			(other.GetComponent<ManipulatorBlock>() == null ||
			other.GetComponent<ManipulatorBlock>().grabbedBlock != transform))
			BlockController.Instance.CollisionWarning(this);
	}
}
