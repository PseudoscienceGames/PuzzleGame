using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildState : State
{
	//public LayerMask m;

	//public override void Activate()
	//{
		
	//}

	//void Update()
	//{
	//	if (Input.GetMouseButtonDown(0))
	//	{
	//		RaycastHit hit;
	//		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 500, m))
	//		{
	//			Selection.Instance.transform.position = hit.transform.position;
	//			Selection.Instance.selected = hit.transform;
	//			Selection.Instance.gameObject.SetActive(true);
	//			CameraControl.Instance.FocusCam(hit.transform.position);
	//		}
	//		else
	//		{
	//			Selection.Instance.selected = null;
	//			Selection.Instance.gameObject.SetActive(false);
	//		}
	//	}
	//}

	//public void Play()
	//{
	//	GameObject.Find("Cursor").SetActive(false);
	//	GameObject.Find("BuildUI").SetActive(false);
	//	GetComponent<StateMachine>().ChangeState<PlayState>();
	//}
}
