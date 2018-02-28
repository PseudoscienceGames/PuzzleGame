using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionToolState : ToolState
{
	public List<Block> selected = new List<Block>();
	private Dictionary<Block, GameObject> selectedMarkers = new Dictionary<Block, GameObject>();
	public GameObject selectedMarker;
	public LayerMask m;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 200, m))
			{

				Block b = hit.transform.GetComponent<Block>();
				if (Input.GetKey(KeyCode.LeftShift))
				{
					if (selected.Contains(b))
						RemoveFromSelected(b);
					else
						AddToSelected(b);
				}
				else
				{
					ClearSelected();
					AddToSelected(b);
				}
			}
			else
				ClearSelected();
		}
	}

	void AddToSelected(Block b)
	{
		selected.Add(b);
		GameObject m = Instantiate(selectedMarker, b.transform.position, Quaternion.identity) as GameObject;
		selectedMarkers.Add(b, m);
	}

	void RemoveFromSelected(Block b)
	{
		selected.Remove(b);
		DestroyImmediate(selectedMarkers[b]);
		selectedMarkers.Remove(b);
	}

	void ClearSelected()
	{
		selected.Clear();
	}
}
