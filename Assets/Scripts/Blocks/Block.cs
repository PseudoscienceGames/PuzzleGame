using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	public Vector3Int loc;
	public bool grabbed;

	public virtual void Activate(float time)
	{
		
	}

	public virtual bool CheckToActivate()
	{
		return false;
	}

	public void Move(Vector3 dir, float time)
	{
		transform.position = loc + (dir * time);
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
