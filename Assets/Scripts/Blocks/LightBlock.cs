using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlock : Block
{
	public bool lit;
	public Light myLight;
	public Material glassMat;

	private void Awake()
	{
		glassMat = transform.Find("Glass").GetComponent<Renderer>().material;
	}

	public override void Activate(float time)
	{
		if (!lit)
		{
			myLight.intensity = time;
			Color c = Color.white * time;
			Debug.Log(c);
			glassMat.SetColor("_EmissionColor", c);
			if (time == 1)
				lit = true;
		}
	}
	public override void Deactivate(float time)
	{
		if (lit)
		{
			myLight.intensity = 1 - time;
			Color c = Color.white;
			glassMat.SetColor("_EmissionColor", c * (1 - time));
			if (time == 1)
				lit = false;
		}
	}
}
