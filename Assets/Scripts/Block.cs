using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	public GridLoc gridLoc;
	public bool powered;
	public bool powerSource;
	public bool transfersPower;
	public bool moving;

	private void Start()
	{
		gridLoc = new GridLoc(transform.position);
	}

	public void StartMove(GridLoc dir)
	{
		if (!moving)
			StartCoroutine("Move", dir);
	}
	//public void StartTurn(Vector3 dir)
	//{
	//	if(!moving)
	//		StartCoroutine("Turn", dir);
	//}

	IEnumerator Move(GridLoc dir)
	{
		moving = true;
		float amt = 0;
		Vector3 initPos = transform.position;
		while (amt < 1)
		{
			amt += Time.deltaTime;
			if (amt > 1)
				amt = 1;
			transform.position = Vector3.Lerp(initPos, initPos + dir.ToWorld(), amt);
			yield return null;
		}
		GameObject.Find("BlockController").GetComponent<BlockController>().blocks.Remove(gridLoc);
		gridLoc += dir;
		GameObject.Find("BlockController").GetComponent<BlockController>().blocks.Add(gridLoc, this);
		moving = false;
		Debug.Log("idk");
		//yield return null;
	}
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

	public virtual void Act()
	{

	}
	public void EndMove()
	{
		StopAllCoroutines();
		transform.position = new Vector3(Mathf.Round(transform.position.x),
										 Mathf.Round(transform.position.y),
										 Mathf.Round(transform.position.z));
		moving = false;
	}
}
