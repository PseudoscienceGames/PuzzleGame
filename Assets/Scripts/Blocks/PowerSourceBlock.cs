﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSourceBlock : Block
{
	private void Awake()
	{
		selfPowered = true;
	}
}
