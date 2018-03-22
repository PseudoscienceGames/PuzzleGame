using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBlock : Block
{

	//private void OnCollisionEnter(Collision collision)
	//{
	//	GetComponent<Rigidbody>().useGravity = true;
	//	enabled = false;
	//	BlockController.Instance.CollisionWarning(this);
	//}

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
			else if (BlockController.Instance.blocks.ContainsKey(VectorToInt(loc - transform.up)) || VectorToInt(loc - transform.up).y < 0)
			{
				if (!BlockController.Instance.blocks.ContainsKey(VectorToInt(loc + transform.forward)))
					Move(transform.forward, time);
			}
		}
	}
}