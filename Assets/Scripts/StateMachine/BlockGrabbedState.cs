using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGrabbedState : State
{
	//public LayerMask m;
	//public override void Activate()
	//{
	//	foreach(Transform t in Selection.Instance.GetComponentInChildren<Transform>())
	//	{
	//		if(t.name != "Selector" && t.name != "TranslateArrows")
	//		{
	//			t.gameObject.SetActive(false);
	//		}
	//	}
	//}

	//public void Update()
	//{
	//	if (Input.GetMouseButtonDown(0))
	//	{
	//		StateMachine.Instance.ChangeState<SelectionState>();
	//		Selection.Instance.selected.gameObject.layer = 11;
	//		foreach (Transform t in Selection.Instance.GetComponentInChildren<Transform>())
	//		{
	//			if (t.name != "Selector" && t.name != "TranslateArrows")
	//			{
	//				t.gameObject.SetActive(true);
	//			}
	//		}
	//	}
	//	else
	//	{
	//		RaycastHit hit;
	//		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 500, m))
	//		{
	//			Debug.Log(hit.transform.name);
	//			Vector3 pos = hit.point + (hit.normal * 0.5f);
	//			Selection.Instance.transform.position = new Vector3Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z));
	//			Selection.Instance.selected.position = Selection.Instance.transform.position;
	//		}
	//	}
	//}
}
