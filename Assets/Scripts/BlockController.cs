using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
	//	public List<Block> blocks = new List<Block>();
	public Dictionary<Vector3Int, Block> blocks = new Dictionary<Vector3Int, Block>();
	public List<Block> actingBlocks = new List<Block>();
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

		foreach (Block b in GameObject.FindObjectsOfType<Block>())
		{
			if (b.gameObject.activeSelf)
			{
				Vector3 pos = b.transform.position;
				b.loc = new Vector3Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z));
				b.transform.position = b.loc;
				blocks.Add(b.loc, b);
				if(b.CheckAction())
				{
					actingBlocks.Add(b);
				}
			}
		}

		time = 0;
		while(time < 1)
		{
			time += Time.deltaTime * tickSpeed;
			if (time > 1)
				time = 1;
			foreach (Block b in actingBlocks)
			{
				if (b.GetComponent<BotBlock>() == null)
				{
					b.Activate(time);
				}
			}
			foreach (Block b in actingBlocks)
			{
				if (b.GetComponent<BotBlock>() != null)
				{
					b.Activate(time);
					//Debug.Log(b.name + " " + b.grabbed);
				}
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

