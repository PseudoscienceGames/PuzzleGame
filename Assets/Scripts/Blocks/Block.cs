using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	public Vector3Int loc;
	public Mesh mesh;
	public List<Collider> c;

	public List<Side> sides = new List<Side>();

	public void CheckSides()
	{
		sides.Clear();
		sides = new List<Side>(transform.GetComponentsInChildren<Side>());
		foreach(Side side in sides)
		{
			List<Collider> cols = new List<Collider>(Physics.OverlapSphere(side.transform.position, .2f, 1 << LayerMask.NameToLayer("Side")));
			foreach(Collider col in cols)
			{
				if (col.transform.root != transform)
					side.adjacentSide = col.GetComponent<Side>();
			}
		}
	}
}
