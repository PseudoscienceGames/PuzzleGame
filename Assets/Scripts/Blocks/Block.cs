using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	public Vector3Int loc;
	public Mesh mesh;

	public List<Side> sides = new List<Side>();
	public bool active;

	public virtual void Activate()
	{
		Debug.Log(name);
	}

	public virtual void Deactivate()
	{

	}

	public void CheckSides()
	{
		sides.Clear();
		sides = new List<Side>(transform.GetComponentsInChildren<Side>());
		foreach(Side side in sides)
		{
			List<Collider> cols = new List<Collider>(Physics.OverlapSphere(side.transform.position, .2f, 1 << LayerMask.NameToLayer("Side")));
			side.adjacentSide = null;
			foreach (Collider col in cols)
			{
				if (col.transform.root != transform)
				{
					side.adjacentSide = col.GetComponent<Side>();
				}
			}
		}
	}
}
