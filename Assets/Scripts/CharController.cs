using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
	public float moveSpeed;
	public Vector3 mov;
	private CharacterController c;
	public bool holding = false;

	private void Start()
	{
		c = GetComponent<CharacterController>();
	}

	void Update ()
	{
		if (Input.GetMouseButtonDown(0))
			CheckForMovableBlock();
		if (Input.GetMouseButtonUp(0))
			holding = false;
		if (holding)
		{
			if (Input.GetButtonDown("Vertical"))
			{
				if (Input.GetAxis("Vertical") > 0)
					StartCoroutine("MoveForward");
				if (Input.GetAxis("Vertical") < 0)
					StartCoroutine("MoveBackward");
			}
			if (Input.GetAxis("Hori") > 0)
				StartCoroutine("MoveForward");
			if (Input.GetAxis("Vertical") < 0)
				StartCoroutine("MoveBackward");
		}
		else
		{
			mov = new Vector3(Input.GetAxis("Horizontal"), mov.y, Input.GetAxis("Vertical"));
			if (!c.isGrounded)
				mov += Physics.gravity * Time.deltaTime;
			else
				mov.y = 0;
			c.Move(transform.TransformVector(mov) * Time.deltaTime * moveSpeed);
		}
	}

	void CheckForMovableBlock()
	{
		Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 1f))
		{
			if (hit.transform.tag == "Movable")
			{
				holding = true;
				transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));
			}
		}
	}
}
