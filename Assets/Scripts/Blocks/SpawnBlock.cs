﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlock : Block
{
	public List<GameObject> toSpawn = new List<GameObject>();
	List<GameObject> storedSpawn = new List<GameObject>();
	private int wait;
	public int waitCount;

	public void Init()
	{
		storedSpawn = new List<GameObject>(toSpawn);
	}

	public override bool CheckToActivate()
	{
		if (toSpawn.Count > 0)
		{
			if (wait == waitCount)
				wait = 0;
			if (wait == 0)
			{
				Instantiate(toSpawn[0], loc + transform.up, transform.rotation);
				toSpawn.RemoveAt(0);
			}
			wait++;
		}
		

		return false;
	}

	public override void Reset()
	{
		toSpawn = new List<GameObject>(storedSpawn);
	}
}