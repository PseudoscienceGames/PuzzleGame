using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : MonoBehaviour
{
	public LayerMask blockOnly;
	public LayerMask blockAndSurface;
	public LayerMask rotMask;
	public Transform selectedBlock;
	public GameObject rotTool;

	public static BuildController Instance;
	private void Awake(){ Instance = this; }

	public void Update()
	{
		if (Input.GetKeyUp(KeyCode.Delete) && selectedBlock != null)
		{
			DestroyImmediate(selectedBlock.gameObject);
			Deselect();
		}
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 200, rotMask))
		{
			if (Input.GetMouseButtonDown(0))
			{
				if (hit.transform.name == "XCCW")
					selectedBlock.transform.Rotate(rotTool.transform.forward, -90, Space.World);
				else if (hit.transform.name == "XCW")
					selectedBlock.transform.Rotate(rotTool.transform.forward, 90, Space.World);
				else if (hit.transform.name == "YCCW")
					selectedBlock.transform.Rotate(rotTool.transform.up * -90, Space.World);
				else if (hit.transform.name == "YCW")
					selectedBlock.transform.Rotate(rotTool.transform.up * 90, Space.World);
				else if (hit.transform.name == "ZCCW")
					selectedBlock.transform.Rotate(rotTool.transform.right * 90, Space.World);
				else if (hit.transform.name == "ZCW")
					selectedBlock.transform.Rotate(rotTool.transform.right * -90, Space.World);
			}
		}
		else if (Input.GetMouseButtonDown(0))
		{
			StartCoroutine("MouseDown");
		}
	}

	public IEnumerator MouseDown()
	{
		Debug.Log("Started");
		Vector2 initMousePos = Input.mousePosition;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 200, blockOnly))
		{
			selectedBlock = hit.transform;
			Debug.Log(selectedBlock.name);
			selectedBlock.transform.GetComponent<BoxCollider>().enabled = false;
			rotTool.SetActive(false);
		}
		else
		{
			Deselect();
			yield break;
		}
		Vector3 initPos = selectedBlock.transform.position;
		while (!Input.GetMouseButtonUp(0))
		{
			Debug.Log(selectedBlock.name);
			if (Vector2.Distance(Input.mousePosition, initMousePos) > 10)
			{
				ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out hit, 200, blockAndSurface))
				{
					Vector3 pos = hit.point + (hit.normal * 0.5f);
					pos = new Vector3Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z));
					selectedBlock.position = pos;
				}
			}
			Debug.Log(selectedBlock.name);
			yield return null;
		}
		if (initPos == selectedBlock.transform.position)
			CameraControl.Instance.FocusCam(selectedBlock.position);
		selectedBlock.transform.GetComponent<BoxCollider>().enabled = true;
		transform.position = selectedBlock.position;
		rotTool.SetActive(true);
		yield return null;

	}
	void Deselect()
	{
		selectedBlock = null;
		rotTool.SetActive(false);
	}
}
