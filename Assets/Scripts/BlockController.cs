using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
	public Dictionary<Vector3Int, Block> blocks = new Dictionary<Vector3Int, Block>();
	public List<Block> blocksToActivate = new List<Block>();
	public Dictionary<Block, TempTransform> initLocs = new Dictionary<Block, TempTransform>();
	public float time;
	public float tickSpeed;
	public GameObject buildMenu;
	public GameObject playMenu;
	public bool playing = false;
	public List<Material> colorMats = new List<Material>();

	public static BlockController Instance;
	private void Awake(){ Instance = this; }

	public void Initialize()
	{
		playing = true;
		BuildController.Instance.gameObject.SetActive(false);
		buildMenu.SetActive(false);
		playMenu.SetActive(true);
		initLocs.Clear();
		foreach(Block b in GameObject.FindObjectsOfType<Block>())
		{
			initLocs.Add(b, new TempTransform(b.transform.root.position, b.transform.root.rotation));
			if (b.GetComponent<SpawnBlock>() != null)
				b.GetComponent<SpawnBlock>().Init();
		}
		Debug.Log(initLocs.Count);
		StartCoroutine(Tick());
	}

	IEnumerator Tick()
	{
		TickStart();
		while (time < 1)
		{
			TickMiddle();
			yield return null;
		}
		TickEnd();
		yield return Tick();
	}

	void TickStart()
	{
		blocks.Clear();
		blocksToActivate.Clear();

		foreach (Block b in GameObject.FindObjectsOfType<Block>())
		{
			if (b.gameObject.activeSelf)
			{
				Vector3 pos = b.transform.position;
				b.loc = new Vector3Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z));
				b.transform.position = b.loc;
				if (blocks.ContainsKey(b.loc))
				{
					Debug.Log(b.name + " " + blocks[b.loc].name);
					CollisionWarning(b);
				}
				else
					blocks.Add(b.loc, b);
			}
		}
		foreach (Block b in blocks.Values)
		{
			if (b.TickStart())
				blocksToActivate.Add(b);
		}
	}

	void TickMiddle()
	{
		time += Time.deltaTime * tickSpeed;
		if (time > 1)
			time = 1;
		foreach (Block b in blocksToActivate)
		{
			b.Tick(time);
		}
	}

	void TickEnd()
	{
		foreach (Block b in blocksToActivate)
		{
			b.TickEnd();
		}
	}

	public void Stop()
	{
		playing = false;
		Time.timeScale = 1;
		StopAllCoroutines();
		BuildController.Instance.gameObject.SetActive(true);
		buildMenu.SetActive(true);
		playMenu.SetActive(false);
		List<Block> blocksToDelete = new List<Block>();
		foreach(Block b in GameObject.FindObjectsOfType<Block>())
		{
			if (initLocs.ContainsKey(b))
				ResetBlock(b);
			else if(b.name != "InvisBlock")
				blocksToDelete.Add(b);
		}
		foreach (Block b in blocksToDelete)
			DeleteBlock(b);
	}

	public void DeleteBlock(Block b)
	{
		blocks.Remove(b.loc);
		blocksToActivate.Remove(b);
		DestroyImmediate(b.gameObject);
	}

	public void ResetBlock(Block b)
	{
		b.gameObject.SetActive(true);
		b.Reset();
		b.transform.position = initLocs[b].pos;
		Vector3 pos = b.transform.position;
		b.loc = new Vector3Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z));
		b.transform.rotation = initLocs[b].rot;
	}

	public void CollisionWarning(Block b)
	{
		StopAllCoroutines();
	}

	public void CheckSuccess()
	{
		bool done = true;
		foreach(ExitBlock e in GameObject.FindObjectsOfType<ExitBlock>())
		{
			if (e.successCount > 0)
				done = false;
		}
		if (done)
			NextLevel();
	}

	void NextLevel()
	{
		Debug.Log("SUCCESS!");
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

