using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGrid : MonoBehaviour
{
	public Dictionary<GridLoc, Block> blocks = new Dictionary<GridLoc, Block>();
	public List<Block> blocksChecked = new List<Block>();

	public static MyGrid instance;
	public static float tickTime = 0.25f;
	private void Awake() { instance = this; }
	private void Start() { InvokeRepeating("Tick", 0, tickTime); }

	private void Tick()
	{
		Debug.Log("Tick");
		foreach(Block b in blocks.Values)
		{
			b.EndTick();
		}
		blocks.Clear();
		blocksChecked.Clear();
		foreach(Block b in FindObjectsOfType<Block>())
		{
			blocks.Add(b.SetGridLoc(), b);
		}
		foreach(PowerSourceBlock p in FindObjectsOfType<PowerSourceBlock>())
		{
			p.Activate();
		}
	}

}
