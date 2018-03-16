using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlock : Block
{
	public List<GameObject> toSpawn = new List<GameObject>();
	List<GameObject> storedSpawn = new List<GameObject>();
	public bool wait = false;

	public void Init()
	{
		storedSpawn = new List<GameObject>(toSpawn);
	}

	public override bool CheckToActivate()
	{
		if(wait)
		{
			wait = false;
		}
		else if (toSpawn.Count > 0)
		{
			Instantiate(toSpawn[0], loc + transform.up, transform.rotation);
			toSpawn.RemoveAt(0);
			wait = true;
		}
		

		return false;
	}

	public override void Reset()
	{
		toSpawn = new List<GameObject>(storedSpawn);
	}
}
