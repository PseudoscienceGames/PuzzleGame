using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Side : MonoBehaviour
{
	public Type type;
	public Side adjacentSide;
}

public enum Type
{
	Base,
	Power

}