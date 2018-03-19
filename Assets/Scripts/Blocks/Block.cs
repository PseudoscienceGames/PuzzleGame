using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	public Vector3Int loc;
	public bool grabbed;
	public bool locked;
	public int color;//0=any 1=red 2=orange 3=yellow 4=green 5=blue 6=purple

	public virtual void Activate(float time)
	{
		
	}

	public virtual bool CheckToActivate()
	{
		return false;
	}

	public void Move(Vector3 dir, float time)
	{
		GetComponent<Rigidbody>().MovePosition(loc + (dir * time));
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
}
