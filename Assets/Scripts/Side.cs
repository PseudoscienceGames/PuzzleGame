using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Side : MonoBehaviour
{
	public SideType type;
	public Side adjacentSide;
}

public enum SideType
{
	Base,
	Power

}