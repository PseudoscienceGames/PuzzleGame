using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
	public List<Block> blocks = new List<Block>();
	public List<Block> blocksToActivate = new List<Block>();
	public float tickTime;

	public static BlockController Instance;
	private void Awake(){ Instance = this; }

	public void Initialize()
	{
		Tick();
	}

	IEnumerator Tick()
	{
		List<Block> blocksToCheck = new List<Block>();
		List<Block> checkedBlocks = new List<Block>();
		blocksToActivate.Clear();
		foreach (Block b in blocks)
		{
			Vector3 pos = b.transform.position;
			b.loc = new Vector3Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z));
			b.transform.position = b.loc;
		}
		foreach (Block b in blocks)
		{
			b.CheckSides();
			if (b.GetComponent<PowerSourceBlock>() != null)
				blocksToCheck.Add(b);
		}
		while(blocksToCheck.Count > 0)
		{
			Block b = blocksToCheck[0];
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

		float time = 0;
		while(time >= 1)
		{
			time += Time.deltaTime;
			if (time > 1)
				time = 1;
			foreach(Block b in blocks)
			{
				if (blocksToActivate.Contains(b))
					b.Activate(time);
				else
					b.Deactivate(time);
			}
			yield return null;
		}
		yield return Tick();
	}
}
