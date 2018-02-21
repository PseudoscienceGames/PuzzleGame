using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
	public Transform corners;
	public MeshRenderer hover;
	public Material hoverMat;
	public Material noHoverMat;
	public Transform selected;

	public static Selection Instance;
	private void Awake() { Instance = this; }

	void Update ()
	{
		Vector2 cam = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.z) - new Vector2(transform.position.x, transform.position.z);
		if(cam.x >= 0)
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
			if(Input.GetMouseButtonDown(0))
			{
				if(hit.transform.name == "TranslateArrows")
				{
					selected.gameObject.layer = 2;
					StateMachine.Instance.ChangeState<BlockGrabbedState>();
				}
				if (hit.transform.name == "XCCWArrow")
					selected.transform.Rotate(transform.right, 90, Space.World);
				else if (hit.transform.name == "XCWArrow")
					selected.transform.Rotate(transform.right, -90, Space.World);
				else if (hit.transform.name == "YCCWArrow")
					selected.transform.Rotate(transform.up * -90, Space.World);
				else if (hit.transform.name == "YCWArrow")
					selected.transform.Rotate(transform.up * 90, Space.World);
				else if (hit.transform.name == "ZCCWArrow")
					selected.transform.Rotate(transform.forward * -90, Space.World);
				else if (hit.transform.name == "ZCWArrow")
					selected.transform.Rotate(transform.forward * 90, Space.World);
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
