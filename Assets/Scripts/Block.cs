using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	public GridLoc gridLoc;
	public bool powered;
	public bool powerSource;
	public bool transfersPower;

	private void Start()
	{
		gridLoc = new GridLoc(transform.position);
	}

	//public void StartMove(Vector3 dir)
	//{
	//	if(!moving)
	//		StartCoroutine("Move", dir);
	//}
	//public void StartTurn(Vector3 dir)
	//{
	//	if(!moving)
	//		StartCoroutine("Turn", dir);
	//}

	//IEnumerator Move(Vector3 dir)
	//{
	//	moving = true;
	//	float amt = 0;
	//	Vector3 initPos = transform.position;
	//	while (amt < 1)
	//	{
	//		amt += Time.deltaTime;
	//		if (amt > 1)
	//			amt = 1;
	//		transform.position = Vector3.Lerp(initPos, initPos + dir, amt);
	//		yield return null;
	//	}
	//	moving = false;
	//	//yield return null;
	//}
	//IEnumerator Turn(Vector3 dir)
	//{
	//	moving = true;
	//	dir *= 90;
	//	float amt = 0;
	//	Vector3 initRot = transform.eulerAngles;
	//	while (amt < 1)
	//	{
	//		amt += Time.deltaTime;
	//		if (amt > 1)
	//			amt = 1;
	//		transform.eulerAngles = Vector3.Lerp(initRot, initRot + dir, amt);
	//		yield return null;
	//	}
	//	moving = false;
	//	//yield return null;
	//}

	//public virtual void Act()
	//{
	//	StopAllCoroutines();
	//	transform.position = new Vector3(Mathf.Round(transform.position.x),
	//									 Mathf.Round(transform.position.y),
	//									 Mathf.Round(transform.position.z));
	//	moving = false;
	//}
}
