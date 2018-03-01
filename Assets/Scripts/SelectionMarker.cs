using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionMarker : MonoBehaviour
{
	void Update ()
	{
		transform.localScale = Vector3.one * (1 - Mathf.PingPong(Time.time * 0.1f, .05f));
	}
}
