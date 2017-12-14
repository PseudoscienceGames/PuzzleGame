using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
	public List<GameObject> availBlocks = new List<GameObject>();
	public int selected;

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
			transform.position = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), Mathf.Round(pos.z));
		}
		if(!Input.GetMouseButton(1) && Input.GetAxis("Mouse ScrollWheel") != 0)
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
			Instantiate(availBlocks[selected], transform.position, transform.rotation);
		}
	}
}
