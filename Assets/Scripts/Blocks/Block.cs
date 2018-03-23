using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	public Transform grabbedBlock;
	public Vector3Int loc;
	public bool grabbed;
	public bool locked;
	public int color;//0=any 1=red 2=orange 3=yellow 4=green 5=blue 6=purple
	public bool canChangeColor;
	public Transform moveyBit;

	public virtual bool TickStart()
	{
		return false;
	}

	public virtual void Tick(float time)
	{
		
	}

	public virtual void TickEnd()
	{
		grabbed = false;
	}

	public void Move(Vector3 dir, float time, bool push)
	{
		transform.position = loc + (dir * time);
		if (push && BlockController.Instance.blocks.ContainsKey(VectorToInt(loc + dir)) &&
			BlockController.Instance.blocks[VectorToInt(loc + dir)].name != "InvisBlock")
		{
			Block b = BlockController.Instance.blocks[VectorToInt(loc + dir)];
			b.Move(dir, time, true);
			b.grabbed = true;
			if (BlockController.Instance.blocksToActivate.Contains(b))
				BlockController.Instance.blocksToActivate.Remove(b);
		}

	}

	public virtual void Reset()
	{
		transform.parent = null;
		grabbed = false;
	}

	public Vector3Int VectorToInt(Vector3 v)
	{
		Vector3Int i = new Vector3Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
		return i;
	}

	public void CycleColor(int i)
	{
		color += i;
		if (color > 6)
			color = 0;
		if (color < 0)
			color = 6;
		moveyBit.GetComponent<Renderer>().material = BlockController.Instance.colorMats[color];
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Col") && other.transform != grabbedBlock
			&& (other.GetComponent<Block>() == null || other.GetComponent<Block>().grabbedBlock != transform))
			BlockController.Instance.CollisionWarning(this);
	}
}
