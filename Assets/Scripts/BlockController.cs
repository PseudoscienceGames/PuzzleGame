using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
	public List<Block> blocks = new List<Block>();

	public static BlockController Instance;
	private void Awake(){ Instance = this; }

	public void Initialize()
	{
		foreach(Block b in blocks)
		{
			Vector3 pos = b.transform.position;
			b.loc = new Vector3Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z));
			b.CheckSides();
		}
	}
}
