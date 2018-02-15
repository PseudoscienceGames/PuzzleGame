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
		}
		Tick();
	}

	public void Tick()
	{
		List<Block> blocksToCheck = new List<Block>();
		List<Block> checkedBlocks = new List<Block>();
		List<Block> blocksToActivate = new List<Block>();

		foreach (Block b in blocks)
		{
			b.CheckSides();
			if (b.GetComponent<PowerSourceBlock>() != null)
				blocksToCheck.Add(b);
		}
		while(blocksToCheck.Count > 0)
		{
			Block b = blocksToCheck[0];
			Debug.Log(blocksToCheck.Count);
			foreach (Side s in b.sides)
			{
				if(s.adjacentSide != null && s.type == s.adjacentSide.type && s.type != SideType.Base && !checkedBlocks.Contains(s.adjacentSide.transform.root.GetComponent<Block>()))
				{
					blocksToCheck.Add(s.adjacentSide.transform.root.GetComponent<Block>());
				}
			}
			blocksToActivate.Add(b);
			checkedBlocks.Add(b);
			blocksToCheck.RemoveAt(0);
		}
		foreach(Block b in blocksToActivate)
		{
			b.Activate();
		}
	}
}
