using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
	public List<GameObject> availBlocks = new List<GameObject>();
	public int selected;
	public Vector3Int loc;
	public float smoothTime;
	private Vector3 velocity = Vector3.zero;
	private LayerMask ignoreSides;

	void Start()
	{
		GetComponent<MeshFilter>().sharedMesh = availBlocks[selected].GetComponent<Block>().mesh;
		ignoreSides = LayerMask.NameToLayer("Sides");
	}

	void Update ()
	{
		RaycastHit hit;
		Vector3 pos;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 500, ignoreSides))
			Debug.Log(hit.transform.gameObject.layer);
		{
			pos = hit.point + (hit.normal * 0.5f);
			loc = new Vector3Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z));
			transform.position = Vector3.SmoothDamp(transform.position, loc, ref velocity, smoothTime);

		}
		if(Input.GetMouseButton(1) && Input.GetAxis("Mouse ScrollWheel") != 0)
		{
			if(Input.GetAxis("Mouse ScrollWheel") > 0)
			{
				selected++;
				if (selected > availBlocks.Count - 1)
					selected = 0;
			}
			if (Input.GetAxis("Mouse ScrollWheel") < 0)
			{
				selected--;
				if (selected < 0)
					selected = availBlocks.Count - 1;
			}
			GetComponent<MeshFilter>().sharedMesh = availBlocks[selected].GetComponent<Block>().mesh;
		}
		if(Input.GetMouseButtonDown(0))
		{
			GameObject b = Instantiate(availBlocks[selected], loc, transform.rotation) as GameObject;
			BlockController.Instance.blocks.Add(b.GetComponent<Block>());
		}
	}

	IEnumerator Move(Vector3 moveTo)
	{
		float t = 0;
		Vector3 initPos = transform.position;
		while (t < 1)
		{
			t += Time.deltaTime * 30;
			transform.position = Vector3.Lerp(initPos, moveTo, t);
			yield return null;
		}
		yield return null;
	}
}
