using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlock : Block
{
	public bool lit;

	public override void Activate(float time)
	{
		if (!lit)
		{
			transform.Find("Light").GetComponent<Light>().intensity = time;
			if (time == 1)
				lit = true;
		}
	}
	public override void Deactivate(float time)
	{
		if (lit)
		{
			transform.Find("Light").GetComponent<Light>().intensity = 1 - time;
			if (time == 1)
				lit = false;
		}
	}
}
