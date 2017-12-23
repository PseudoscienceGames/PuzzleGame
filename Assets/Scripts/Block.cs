using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	public GridLoc gridLoc;
	public bool takesPower;
	public bool givesPower;
	public bool hasPower;
	public float t;

	private void Start()
	{
		gridLoc = new GridLoc(transform.position);
		//StartMove(new GridLoc(transform.forward));
	}

	public void StartMove(GridLoc dir)
	{
		gridLoc += dir;
		StartCoroutine("Move", gridLoc);
	}

	IEnumerator Move(GridLoc g)
	{
		Vector3 initPos = transform.position;
		t = 0;
		while (t < 1)
		{
			t += Time.deltaTime;
			Debug.Log(Time.deltaTime);
			transform.position = Vector3.Lerp(initPos, gridLoc.ToVector3(), t);
			yield return null;
		}
		yield return null;
	}


	public void EndMove()
	{
		StopAllCoroutines();
		GridController.GC.newGrid.Add(gridLoc, this);
		transform.position = new GridLoc(transform.position).ToVector3();
		//hasPower = false;
	}

	public virtual void Action()
	{

	}
}