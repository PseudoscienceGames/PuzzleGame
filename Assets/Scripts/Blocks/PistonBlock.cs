using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonBlock : Block
{
	public Transform piston;
	public GameObject pistonBlock;
	public bool extended = false;

	private void Awake()
	{
		piston = transform.Find("Piston");
	}

	public override void Activate(float time)
	{
		if (!extended)
		{
			piston.transform.position = (piston.transform.up * time) + transform.position;
			if (time == 1)
			{
				extended = true;
				pistonBlock.SetActive(true);
			}
		}
	}
	public override void Deactivate(float time)
	{
		if (extended)
		{
			piston.transform.position = (piston.transform.up * (1 - time)) + transform.position;
			if (time == 1)
			{
				extended = false;
				pistonBlock.SetActive(false);
			}
		}
	}

}
