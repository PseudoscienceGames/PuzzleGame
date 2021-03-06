﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	public Vector3Int loc;
	public int color;//0=any 1=red 2=orange 3=yellow 4=green 5=blue 6=purple
	public bool locked;

	public virtual bool TickStart()
	{
		return false;
	}

	public virtual void Tick(float time)
	{
		
	}

	public virtual void TickEnd()
	{
		
	}

	public void Move(Vector3 dir, float time)
	{
		transform.position = loc + (dir * time);
	}

	public virtual void Reset()
	{

	}

	public Vector3Int VectorToInt(Vector3 v)
	{
		Vector3Int i = new Vector3Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
		return i;
	}
}
