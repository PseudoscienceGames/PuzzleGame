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

		holding = true;
	}
}
