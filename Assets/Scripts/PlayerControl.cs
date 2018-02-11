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

	void Start()
	{
		GetComponent<MeshFilter>().sharedMesh = availBlocks[selected].GetComponent<MeshFilter>().sharedMesh;
	}

	void Update ()
	{
		RaycastHit hit;
		Vector3 pos;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 500))
		{
			pos = hit.point + (hit.normal * 0.5f);
			loc = new Vector3Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));
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
			GetComponent<MeshFilter>().sharedMesh = availBlocks[selected].GetComponent<MeshFilter>().sharedMesh;
		}
		if(Input.GetMouseButtonDown(0))
		{
			Instantiate(availBlocks[selected], loc, transform.rotation);
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
