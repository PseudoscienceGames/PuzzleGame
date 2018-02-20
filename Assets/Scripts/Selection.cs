using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
	public Transform corners;
	public MeshRenderer hover;
	public Material hoverMat;
	public Material noHoverMat;

	void Update ()
	{
		Vector2 cam = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.z);
		if(cam.x >= 0)
		{
			if (cam.y >= 0)
				transform.LookAt(Vector3.right);
			else
				transform.LookAt(-Vector3.forward);
		}
		else
		{
			if (cam.y >= 0)
				transform.LookAt(Vector3.forward);
			else
				transform.LookAt(-Vector3.right);
		}
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 200, 1 << LayerMask.NameToLayer("TransformTool")))
		{
			MeshRenderer newHover = hit.transform.GetComponent<MeshRenderer>();
			if(hover != newHover)
			{
				if(hover != null)
					hover.material = noHoverMat;
				newHover.material = hoverMat;
				hover = newHover;
			}
		}
		else
		{
			if (hover != null)
			{
				hover.material = noHoverMat;
				hover = null;
			}
		}

		corners.localScale = Vector3.one * (Mathf.PingPong(Time.time * 0.1f, 0.05f) + 0.975f);
	}
}
