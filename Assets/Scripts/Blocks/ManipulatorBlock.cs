using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManipulatorBlock : Block
{
	public Transform moveyBit;
	public Transform grabbedBlock;

	public void CycleColor(int i)
	{
		color += i;
		if (color > 6)
			color = 0;
		if (color < 0)
			color = 6;
		moveyBit.GetComponent<Renderer>().material = BlockController.Instance.colorMats[color];
	}
}
