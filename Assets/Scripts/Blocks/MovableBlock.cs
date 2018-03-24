using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBlock : Block
{
	public bool grabbed;

	private void OnTriggerEnter(Collider other)
	{
		//redo
		//if (other.gameObject.CompareTag("Col") && other.transform != grabbedBlock
		//	&& (other.GetComponent<Block>() == null || other.GetComponent<Block>().grabbedBlock != transform))
		//	BlockController.Instance.CollisionWarning(this);
	}
}
