using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
	void Update ()
	{
		RaycastHit hit;
		Vector3 pos;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 500))
		{
			pos = hit.point + (hit.normal * 0.5f);
			transform.position = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), Mathf.Round(pos.z));
		}
	}
}
