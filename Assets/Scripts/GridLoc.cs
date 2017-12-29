using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct GridLoc
{
	public int x;
	public int y;
	public int z;

	public GridLoc(int x, int y, int z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}
	public GridLoc(Vector3 worldLoc)
	{
		this.x = Mathf.RoundToInt(worldLoc.x);
		this.y = Mathf.RoundToInt(worldLoc.y);
		this.z = Mathf.RoundToInt(worldLoc.z);
	}
	public Vector3 ToWorld()
	{
		return new Vector3(x, y, z);
	}
	public List<GridLoc> AdjacentBlocks()
	{
		List<GridLoc> a = new List<GridLoc>();
		a.Add(this + new GridLoc(Vector3.up));
		a.Add(this + new GridLoc(-Vector3.up));
		a.Add(this + new GridLoc(Vector3.forward));
		a.Add(this + new GridLoc(-Vector3.forward));
		a.Add(this + new GridLoc(Vector3.right));
		a.Add(this + new GridLoc(-Vector3.right));
		return a;
	}
	public Vector3 ToVector3()
	{
		return new Vector3(x, y, z);
	}
	public static GridLoc operator -(GridLoc a)
	{
		return new GridLoc(-a.x, -a.y, -a.z);
	}
	public static GridLoc operator +(GridLoc a, GridLoc b)
	{
		return new GridLoc(a.x + b.x, a.y + b.y, a.z + b.z);
	}
	public static GridLoc operator -(GridLoc a, GridLoc b)
	{
		return new GridLoc(a.x - b.x, a.y - b.y, a.z - b.z);
	}
	public static bool operator ==(GridLoc a, GridLoc b)
	{
		return a.x == b.x && a.y == b.y && a.z == b.z;
	}
	public static bool operator !=(GridLoc a, GridLoc b)
	{
		return !(a == b);
	}
	public override bool Equals(object obj)
	{
		if (obj is GridLoc)
		{
			GridLoc p = (GridLoc)obj;
			return x == p.x && y == p.y && z == p.z;
		}
		return false;
	}
	public bool Equals(GridLoc p)
	{
		return x == p.x && y == p.y && z == p.z;
	}
	public override int GetHashCode()
	{
		return x ^ y ^ z;
	}
	public override string ToString()
	{
		return string.Format("({0},{1},{2})", x, y, z);
	}

}
