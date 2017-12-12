using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
	public float moveSpeed;
	public Vector3 mov;
	private CharacterController c;
	public bool holding = false;
	public Transform h;
	public bool movF = false;

	private void Start()
	{
		c = GetComponent<CharacterController>();
	}

	void Update ()
	{
		if (Input.GetMouseButtonDown(0))
			CheckForMovableBlock();
		if (Input.GetMouseButtonUp(0))
		{
			holding = false;
			transform.parent = null;
		}
		if (holding)
		{
			if (Input.GetButtonDown("Vertical"))
			{
				if (Input.GetAxis("Vertical") > 0)
					h.GetComponent<BlockBase>().StartMove(FindForward(transform.position, h.transform.position));
				if (Input.GetAxis("Vertical") < 0)
					h.GetComponent<BlockBase>().StartMove(-FindForward(transform.position, h.transform.position));
			}
			if (Input.GetButtonDown("Horizontal"))
			{
				if (Input.GetAxis("Horizontal") > 0)
					h.GetComponent<BlockBase>().StartTurn(-Vector3.up);
				if (Input.GetAxis("Horizontal") < 0)
					h.GetComponent<BlockBase>().StartTurn(Vector3.up);
			}
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
				h = hit.transform;
				transform.parent = h;
			}
		}
	}

	Vector3 FindForward(Vector3 cPos, Vector3 hPos)
	{
		Vector3 dir = new Vector3(hPos.x - cPos.x, 0, hPos.z - cPos.z);
		return dir;
	}
}
