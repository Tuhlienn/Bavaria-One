using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coord {
	public int x;
	public int y;

	public Coord(int x, int y)
    {
		this.x = x;
		this.y = y;
	}

    public Coord(Vector2 position)
    {
        this.x = (int)position.x;
        this.y = (int)position.y;
    }

    public Vector2 toVector2()
    {
        return new Vector2(x, y);
    }

    public override bool Equals(object obj)
    {
        Coord other = obj as Coord;
        if (ReferenceEquals(other, null))
            return false;
        return x == other.x && y == other.y;
    }

    public override int GetHashCode()
    {
        return x.GetHashCode() ^ y.GetHashCode();
    }

    public double euclidianDistance(Coord other){
		if (ReferenceEquals (other, null))
			return -1;
		return Mathf.Sqrt(Mathf.Pow(x-other.x, 2) + Mathf.Pow(y - other.y, 2));
	}

	public int manhattanDistance(Coord other){
		if (ReferenceEquals (other, null))
			return -1;
		return (Mathf.Abs (x - other.x) + Mathf.Abs (y - other.y));
	}
}             
