﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour
{
	public float zoom;
	public int zoomMin;
	public int zoomMax;
	public float zoomSpeed;
	public float cameraRotSpeed;
	public float camPanSpeed;
	public int scrollAreaSize;
	public bool screenEdgeScroll;

	public GameObject cameraPivot;

	public LayerMask mask;
	public Vector3 oldPos;

	public float smoothTime;
	private Vector3 velocity = Vector3.zero;

	public static CameraControl Instance;
	void Awake() { Instance = this; }

	void Update()
	{
		if (Input.GetAxis("Vertical") != 0)
			transform.position += transform.forward * Input.GetAxisRaw("Vertical") * camPanSpeed;
		if (Input.GetAxis("Horizontal") != 0)
			transform.position += transform.right * Input.GetAxisRaw("Horizontal") * camPanSpeed;
		//Pivots camera based on mouse movement
		if (Input.GetMouseButton(1))
		{
			transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * cameraRotSpeed * Time.deltaTime, Space.Self);
			cameraPivot.transform.Rotate(-Vector3.right, Input.GetAxis("Mouse Y") * cameraRotSpeed * Time.deltaTime);
			//Zoom camera
			zoom = -Camera.main.transform.localPosition.z;
			if (zoom > zoomMin && zoom < zoomMax)
			{
				if (Camera.main.orthographic)
					Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
				else
				{
					zoom += -(Input.GetAxisRaw("Mouse ScrollWheel")) * zoomSpeed * zoom;
					if (zoom > zoomMin && zoom < zoomMax)
					{
						Camera.main.transform.localPosition = new Vector3(0, 0, -zoom);
					}
				}
			}
		}
		if (Input.GetMouseButtonDown(2))
		{
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 500, mask))
				oldPos = hit.point;
		}
	}

	private void LateUpdate()
	{
		if (Input.GetMouseButton(2))
		{
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 500, mask))
			{
				Vector3 targetPosition = transform.position + (oldPos - hit.point);
				transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
			}
		}
	}
}