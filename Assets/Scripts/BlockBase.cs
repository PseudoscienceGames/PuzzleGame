using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBase : MonoBehaviour
{
	public void StartMove(Vector3 dir)
	{
		StartCoroutine("Move", dir);
	}
	public void StartTurn(Vector3 dir)
	{
		StartCoroutine("Turn", dir);
	}

	IEnumerator Move(Vector3 dir)
	{
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
		yield return null;
	}
	IEnumerator Turn(Vector3 dir)
	{
		dir *= 90;
		float amt = 0;
		Vector3 initRot = transform.eulerAngles;
		while (amt < 1)
		{
			amt += Time.deltaTime;
			if (amt > 1)
				amt = 1;
			transform.eulerAngles = Vector3.Lerp(initRot, initRot + dir, amt);
			yield return null;
		}
		yield return null;
	}
}
