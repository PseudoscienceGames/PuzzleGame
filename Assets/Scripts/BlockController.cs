using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
	//	public List<Block> blocks = new List<Block>();
	public Dictionary<Vector3Int, Block> blocks = new Dictionary<Vector3Int, Block>();
	public List<Block> blocksToActivate = new List<Block>();
	public List<Block> blocksToFall = new List<Block>();
	public Dictionary<Block, TempTransform> initLocs = new Dictionary<Block, TempTransform>();
	public float time;
	public float tickSpeed;
	public GameObject buildMenu;
	public GameObject playMenu;

	public static BlockController Instance;
	private void Awake(){ Instance = this; }

	public void Initialize()
	{
		buildMenu.SetActive(false);
		playMenu.SetActive(true);
		initLocs.Clear();
		foreach(Block b in GameObject.FindObjectsOfType<Block>())
		{
			initLocs.Add(b, new TempTransform(b.transform.root.position, b.transform.root.rotation));
		}
		StartCoroutine(Tick());
	}

	public void Stop()
	{
		Time.timeScale = 1;
		StopAllCoroutines();
		buildMenu.SetActive(true);
		playMenu.SetActive(false);
		foreach(Block b in initLocs.Keys)
		{
			ResetBlock(b);
		}
	}

	public void DeleteBlock(Block b)
	{
		blocks.Remove(b.loc);
		DestroyImmediate(b.gameObject);
	}

	IEnumerator Tick()
	{
		blocks.Clear();
		List<Block> blocksToCheck = new List<Block>();
		List<Block> checkedBlocks = new List<Block>();
		blocksToActivate.Clear();
		blocksToFall.Clear();

		foreach (Block b in GameObject.FindObjectsOfType<Block>())
		{
			if (b.gameObject.activeSelf)
			{
				Vector3 pos = b.transform.position;
				b.loc = new Vector3Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z));
				b.transform.position = b.loc;
				blocks.Add(b.loc, b);
			}
		}

		foreach (Block b in blocks.Values)
		{
			Vector3 pos = b.transform.position;
			b.loc = new Vector3Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z));
			b.transform.position = b.loc;
		}
		foreach (Block b in blocks.Values)
		{
			b.CheckSides();
			if (b.selfPowered)
				blocksToCheck.Add(b);
			if (b.DirToSide(-Vector3.up).adjacentSide == null && b.loc.y > 0)
				blocksToFall.Add(b);
		}
		while(blocksToCheck.Count > 0)
		{
			Block b = blocksToCheck[0];
			foreach (Side s in b.sides)
			{
				if(s.adjacentSide != null && s.type == s.adjacentSide.type && s.type != SideType.Base && !checkedBlocks.Contains(s.adjacentSide.transform.root.GetComponent<Block>()))
				{
					if(s.type != SideType.Gear || s.transform.root.up == s.adjacentSide.transform.root.up || s.transform.root.up == -s.adjacentSide.transform.root.up)
						blocksToCheck.Add(s.adjacentSide.transform.root.GetComponent<Block>());
					if (s.type == SideType.Gear)
					{
						if (s.transform.root.up == -s.adjacentSide.transform.root.up)
							s.adjacentSide.transform.root.GetComponent<Block>().dir = s.transform.root.GetComponent<Block>().dir;
						else
							s.adjacentSide.transform.root.GetComponent<Block>().dir = -s.transform.root.GetComponent<Block>().dir;
					}
						if (s.type == SideType.Axle)
					{
						if(s.transform.root.rotation == s.adjacentSide.transform.root.rotation)
							s.adjacentSide.transform.root.GetComponent<Block>().dir = s.transform.root.GetComponent<Block>().dir;
						else
							s.adjacentSide.transform.root.GetComponent<Block>().dir = -s.transform.root.GetComponent<Block>().dir;
					}
				}
			}
			blocksToActivate.Add(b);
			checkedBlocks.Add(b);
			blocksToCheck.RemoveAt(0);
		}

		time = 0;
		while(time < 1)
		{
			time += Time.deltaTime * tickSpeed;
			if (time > 1)
				time = 1;
			foreach(Block b in blocks.Values)
			{
				if (blocksToActivate.Contains(b))
					b.Activate(time);
				else
					b.Deactivate(time);
				if (blocksToFall.Contains(b))
					b.Move(Vector3.down, time);
			}
			yield return null;
		}
		yield return Tick();
	}

	public void ResetBlock(Block b)
	{
		b.Deactivate(1);
		b.transform.position = initLocs[b].pos;
		b.transform.rotation = initLocs[b].rot;
	}

	public void CollisionWarning(Block b)
	{
		StopAllCoroutines();
	}
}

public struct TempTransform
{
	public Vector3 pos;
	public Quaternion rot;

	public TempTransform(Vector3 pos, Quaternion rot)
	{
		this.pos = pos;
		this.rot = rot;
	}
}

