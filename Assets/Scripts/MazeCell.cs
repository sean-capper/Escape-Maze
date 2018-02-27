using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell {
	public int row, col;
	public bool visited = false;
	public GameObject parent, northWall, southWall, eastWall, westWall, floor;
}
