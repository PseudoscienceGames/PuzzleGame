using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlock : Block
{
	public Material on;
	public Material off;

	public override void Activate(float time)
	{
		transform.Find("Light").GetComponent<Light>().intensity = time;
	}
	public override void Deactivate(float time)
	{
		transform.Find("Light").GetComponent<Light>().intensity = 1 - time;
	}
}
