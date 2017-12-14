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
	public bool moving = false;

	private void Start()
	{
		c = GetComponent<CharacterController>();
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
			CheckForMovableBlock();
		if (Input.GetMouseButtonUp(0))
		{
			holding = false;
		}
		if (holding && !moving)
		{
			if (Input.GetButtonDown("Vertical"))
			{
				if (Input.GetAxis("Vertical") > 0)
				{
					Vector3 dir = FindForward(transform.position, h.transform.position);
					h.GetComponent<BlockBase>().StartMove(dir);
					StartCoroutine("Move", dir);
				}
				if (Input.GetAxis("Vertical") < 0)
				{
					Vector3 dir = -FindForward(transform.position, h.transform.position);
					h.GetComponent<BlockBase>().StartMove(dir);
					StartCoroutine("Move", dir);
				}
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
		if (Physics.Raycast(ray, out hit, 1f))
		{
			if (hit.transform.tag == "Movable")
			{
				holding = true;
				transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));
				h = hit.transform;
			}
		}
	}

	Vector3 FindForward(Vector3 cPos, Vector3 hPos)
	{
		Vector3 dir = new Vector3(hPos.x - cPos.x, 0, hPos.z - cPos.z);
		if (Mathf.Abs(dir.x + dir.y + dir.z) != 1)
			Debug.Log("Somethings fucked" + dir);
		return dir;
	}

	IEnumerator Move(Vector3 dir)
	{
		moving = true;
		float amt = 0;
		Vector3 initPos = transform.position;
		while (amt < 1)
		{
			amt += Time.deltaTime;
			if (amt > 1)
				amt = 1;
			transform.position = Vector3.Lerp(initPos, initPos + dir, amt);
			yield return null;
		}
		moving = false;
		yield return null;
	}
}
