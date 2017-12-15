using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BlockController : MonoBehaviour
{
	public List<Block> blocks = new List<Block>();
	public float tickTime = 1f;

	void Start ()
	{
		blocks = GameObject.FindObjectsOfType<Block>().ToList<Block>();
		InvokeRepeating("Tick", 0, tickTime);
	}

	void Tick()
	{
		Debug.Log("Tick");
		foreach(Block b in blocks)
		{
			b.Act();
		}
	}
}
