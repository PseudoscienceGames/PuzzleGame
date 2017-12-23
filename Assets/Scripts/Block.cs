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

	public virtual void Start()
	{
		gridLoc = new GridLoc(transform.position);
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
			t += Time.deltaTime / GridController.GC.tickTime;
			transform.position = Vector3.Lerp(initPos, gridLoc.ToVector3(), t);
			yield return null;
		}
		yield return null;
	}


	public void EndMove()
	{
		StopAllCoroutines();
		transform.position = new GridLoc(transform.position).ToVector3();
		gridLoc = new GridLoc(transform.position);
		GridController.GC.newGrid.Add(gridLoc, this);
		
		//hasPower = false;
	}

	public virtual void Action()
	{

	}
}