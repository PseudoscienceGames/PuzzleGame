using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAddButton : MonoBehaviour
{
	public GameObject block;

	public void AddBlock()
	{
		Transform b = (Instantiate(block) as GameObject).transform;
		b.parent = BlockController.Instance.transform;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 200, BuildController.Instance.blockAndSurface))
		{
			Vector3 pos = hit.point + (hit.normal * 0.5f);
			pos = new Vector3Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z));
			b.position = pos;
		}
		else
		{
			Vector3 pos = Camera.main.ScreenToWorldPoint(Vector3.forward * 10 + Input.mousePosition);
			pos = new Vector3Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z));
			b.position = pos;
		}
	}
}
