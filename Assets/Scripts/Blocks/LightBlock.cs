using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlock : Block
{
	public Material on;
	public Material off;

	public override void Activate()
	{
		base.Activate();
		transform.Find("Light").gameObject.SetActive(true);
		transform.Find("Glass").GetComponent<MeshRenderer>().material = on;
	}
	public override void Deactivate()
	{
		base.Deactivate();
		transform.Find("Light").gameObject.SetActive(false);
		transform.Find("Glass").GetComponent<MeshRenderer>().material = off;
	}
}
