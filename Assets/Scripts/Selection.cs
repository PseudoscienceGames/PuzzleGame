using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
	void Update ()
	{
		transform.localScale = Vector3.one * (Mathf.PingPong(Time.time * 0.1f, 0.05f) + 0.975f);
	}
}
