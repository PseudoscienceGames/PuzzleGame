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
		foreach(Block b in GetComponentsInChildren<Block>(true))
		{
			initLocs.Add(b, new TempTransform(b.transform.position, b.transform.rotation, b.gameObject.activeSelf));
			if (b.GetComponent<SpawnBlock>() != null)
				b.GetComponent<SpawnBlock>().Init();
		}
		StartCoroutine(Tick());
	}

	IEnumerator Tick()
	{
		time = 0;
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

		foreach (PistonBlock b in GetComponentsInChildren<PistonBlock>(true))
		{
			if(!blocks.ContainsValue(b.GetComponent<Block>()))
				CheckBlock(b);
		}
		foreach (FlipperBlock b in GetComponentsInChildren<FlipperBlock>(true))
		{
			if (!blocks.ContainsValue(b.GetComponent<Block>()))
				CheckBlock(b);
		}
		foreach (ConveyorBlock b in GetComponentsInChildren<ConveyorBlock>(true))
		{
			if (!blocks.ContainsValue(b.GetComponent<Block>()))
				CheckBlock(b);
		}
		foreach (Block b in GetComponentsInChildren<Block>(true))
		{
			if (!blocks.ContainsValue(b.GetComponent<Block>()))
				CheckBlock(b);
		}
		foreach (Block b in blocks.Values)
		{
			if (b.TickStart())
				blocksToActivate.Add(b);
		}
	}

	void CheckBlock(Block b)
	{
		if (b.gameObject.activeSelf)
		{
			Vector3 pos = b.transform.position;
			b.loc = new Vector3Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z));
			b.transform.position = b.loc;
			blocks.Add(b.loc, b);
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
		foreach(Block b in GetComponentsInChildren<Block>(true))
		{
			if (initLocs.ContainsKey(b))
				ResetBlock(b);
		}
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
		b.gameObject.SetActive(initLocs[b].active);
	}

	public void CollisionWarning(Block b)
	{
		Debug.Log(b.name);
		StopAllCoroutines();
	}

	public void CheckSuccess()
	{
		bool done = true;
		foreach(ExitBlock e in GetComponentsInChildren<ExitBlock>(true))
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
	public bool active;

	public TempTransform(Vector3 pos, Quaternion rot, bool active)
	{
		this.pos = pos;
		this.rot = rot;
		this.active = active;
	}
}

