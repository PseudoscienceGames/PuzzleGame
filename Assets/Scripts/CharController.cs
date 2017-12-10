using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
	public float moveSpeed;
	private CharacterController c;

	private void Start()
	{
		c = GetComponent<CharacterController>();
	}

	void Update ()
	{
		Vector3 mov = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		c.Move(transform.TransformVector(mov) * Time.deltaTime * moveSpeed);
	}
}
