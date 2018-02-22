using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionState : State
{
	public LayerMask m;

	private void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 200, 1 << LayerMask.NameToLayer("TransformTool")))
		{
			Selection.Instance.Highlight(hit.transform.GetComponent<MeshRenderer>());
			if (Input.GetMouseButtonDown(0))
			{
				if (hit.transform.gameObject.name == "TranslateArrows")
				{
					Selection.Instance.selected.gameObject.layer = 2;
					StateMachine.Instance.ChangeState<BlockGrabbedState>();
				}
				else
					Selection.Instance.RotateBlock(hit.transform.gameObject.name);
			}
		}
		else

		{
			Selection.Instance.RemoveHighlight();

			if (Input.GetMouseButtonDown(0))
			{
				if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 500, m))
				{
					Selection.Instance.transform.position = hit.transform.position;
					Selection.Instance.selected = hit.transform;
					Selection.Instance.gameObject.SetActive(true);
					CameraControl.Instance.FocusCam(hit.transform.position);
					GetComponent<StateMachine>().ChangeState<SelectionState>();
				}
				else
				{
					Selection.Instance.selected = null;
					Selection.Instance.gameObject.SetActive(false);
					GetComponent<StateMachine>().ChangeState<NoSelectionState>();
				}
			}
		}
	}
}
