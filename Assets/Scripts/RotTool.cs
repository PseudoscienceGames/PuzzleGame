using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotTool : MonoBehaviour
{
	public MeshRenderer highlight;
	public Material highlightMat;
	public Material regMat;
	public LayerMask rotTool;
	void Update()
	{
		Vector2 cam = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.z) - new Vector2(transform.position.x, transform.position.z);
		if (cam.x >= 0)
		{
			if (cam.y >= 0)
				transform.LookAt(Vector3.right + transform.position);
			else
				transform.LookAt(-Vector3.forward + transform.position);
		}
		else
		{
			if (cam.y >= 0)
				transform.LookAt(Vector3.forward + transform.position);
			else
				transform.LookAt(-Vector3.right + transform.position);
		}

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 200, rotTool))
			Highlight(hit.transform.GetComponent<MeshRenderer>());
		else
			RemoveHighlight();
	}

	public void Highlight(MeshRenderer r)
	{
		if (highlight != r)
		{
			RemoveHighlight();
			highlight = r;
			highlight.material = highlightMat;
		}
	}

	public void RemoveHighlight()
	{
		if (highlight != null)
		{
			highlight.material = regMat;
			highlight = null;
		}
	}
}
