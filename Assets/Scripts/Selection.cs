using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
	public Transform corners;
	//public MeshRenderer highlight;
	//public Material highlightMat;
	//public Material regMat;
	//public Transform selected;

	//public static Selection Instance;
	//private void Awake() { Instance = this; }

	//public void Highlight(MeshRenderer r)
	//{
	//	if (highlight != r)
	//	{
	//		RemoveHighlight();
	//		highlight = r;
	//		highlight.material = highlightMat;
	//	}
	//}

	//public void RemoveHighlight()
	//{
	//	if (highlight != null)
	//	{
	//		highlight.material = regMat;
	//		highlight = null;
	//	}
	//}

	void Update()
	{
		corners.localScale = Vector3.one * (1 - Mathf.PingPong(Time.time * 0.1f, .05f));
		//Vector2 cam = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.z) - new Vector2(transform.position.x, transform.position.z);
		//if (cam.x >= 0)
		//{
		//	if (cam.y >= 0)
		//		transform.LookAt(Vector3.right + transform.position);
		//	else
		//		transform.LookAt(-Vector3.forward + transform.position);
		//}
		//else
		//{
		//	if (cam.y >= 0)
		//		transform.LookAt(Vector3.forward + transform.position);
		//	else
		//		transform.LookAt(-Vector3.right + transform.position);
		//}
	}

	//public void RotateBlock(string name)
	//{
	//	if (name == "XCCWArrow")
	//		selected.transform.Rotate(transform.right, 90, Space.World);
	//	else if (name == "XCWArrow")
	//		selected.transform.Rotate(transform.right, -90, Space.World);
	//	else if (name == "YCCWArrow")
	//		selected.transform.Rotate(transform.up * -90, Space.World);
	//	else if (name == "YCWArrow")
	//		selected.transform.Rotate(transform.up * 90, Space.World);
	//	else if (name == "ZCCWArrow")
	//		selected.transform.Rotate(transform.forward * -90, Space.World);
	//	else if (name == "ZCWArrow")
	//		selected.transform.Rotate(transform.forward * 90, Space.World);
	//}
}
