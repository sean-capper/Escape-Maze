using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour {
	public GameObject wall;
	[Range(5,40)]
	public int mazeRows, mazeColumns = 20;
	public float wallOffset = 2f;


	private MazeCell[,] maze;
	private MazeCell startingCell;
	private Stack<MazeCell> visitedCells;

	void Start() {
		InitMaze();
		visitedCells = new Stack<MazeCell>();
		startingCell = maze[Random.Range(0,mazeRows), Random.Range(0, mazeColumns)];
		visitedCells.Push(startingCell);
		Carve(startingCell, maze);
	}

	/*
		Initializes the maze by creating MazeCell objects, and creating
		the maze objects in the game world.
	 */
	void InitMaze() {
		maze = new MazeCell[mazeRows, mazeColumns];
		for(int r = 0; r < mazeRows; r++) {
			for(int c = 0; c < mazeColumns; c++) {
				maze[r,c] = new MazeCell();
				maze[r,c].row = r;
				maze[r,c].col = c;
				maze[r,c].parent = new GameObject();
				maze[r,c].parent.transform.position = new Vector3(r, 0, c);
				maze[r,c].parent.name = "Cell " + r + " ," + c;

				maze[r,c].floor = Instantiate(wall, new Vector3(r*wallOffset, -(wallOffset/2f),c*wallOffset), Quaternion.identity);
				maze[r,c].floor.name = "Floor "+ r + ","+c;
				maze[r,c].floor.transform.Rotate(Vector3.right, 90f);
				maze[r,c].floor.transform.parent = maze[r,c].parent.transform;
				
				// make the floor black for now
				maze[r,c].floor.GetComponent<Renderer>().material.color = Color.black;


				// only spawns a west wall if its the first column
				if(c == 0) {
					maze[r,c].westWall = Instantiate(wall, new Vector3(r*wallOffset, 0, (c*wallOffset)-(wallOffset/2f)), Quaternion.identity);
					maze[r,c].westWall.name = "WestWall " + r + "," + c;
					maze[r,c].westWall.transform.parent = maze[r,c].parent.transform;

				}

				maze[r,c].eastWall = Instantiate(wall, new Vector3(r*wallOffset, 0, (c*wallOffset)+(wallOffset/2f)), Quaternion.identity);
				maze[r,c].eastWall.name = "EastWall " + r + "," + c;
				maze[r,c].eastWall.transform.parent = maze[r,c].parent.transform;

				// only spawns a north wall if its the first row
				if(r == 0) {
					maze[r,c].northWall = Instantiate(wall, new Vector3((r*wallOffset)-(wallOffset/2f), 0, c*wallOffset), Quaternion.identity);
					maze[r,c].northWall.name = "NorthWall " + r + "," + c;
					maze[r,c].northWall.transform.Rotate(Vector3.up * 90f);
					maze[r,c].northWall.transform.parent = maze[r,c].parent.transform;

				}

				maze[r,c].southWall = Instantiate(wall, new Vector3((r*wallOffset)+(wallOffset/2f), 0, c*wallOffset), Quaternion.identity);
				maze[r,c].southWall.name = "SouthWall " + r + "," + c;
				maze[r,c].southWall.transform.Rotate(Vector3.up * 90f);
				maze[r,c].southWall.transform.parent = maze[r,c].parent.transform;

				maze[r,c].parent.transform.parent = transform;
			}
		}
	}

	/*
		This is a recursive backtracing method used to create the maze
		The algorithmn works like this:

		- Pick a starting cell
		- Mark the cell as visited and push it to the stack
		- Choose a random, non-visisted neighbor cell
			- If there are no valid neighbors, call the method from the last visited cell
		- Delete the wall connecting currentCell and nextCell
		- Call the method on nextCell until all cells have been visited
 		
	 */
	void Carve(MazeCell currentCell, MazeCell[,] grid) {

			// all cells have been visited, end the method
			if(visitedCells.Count == 0)
				return;
			else {		
				grid[currentCell.row, currentCell.col].visited = true;
				// gets a list of valid neighbors
				List<MazeCell> neighbors = GetNeighbors(currentCell, grid);
				// if there are no valid neighbors, call Carve using the last visited cell
				if(neighbors.Count == 0) {
					Carve(visitedCells.Pop(), grid);
				} 
				// else pick a random neighbor, push it to the stack, and carve a passage
				// then call Carve(...) on the nextCell
				else {
					MazeCell nextCell = neighbors[Random.Range(0, neighbors.Count)];
					visitedCells.Push(nextCell);
					// destory the connecting wall
					DestroyWall(currentCell, nextCell);
					Carve(nextCell, grid);
				}
				
			}
		
	}

	// checks to make sure a cell is inside the grid, and has not been visited
	bool isValidCell(int nextRow, int nextCol, MazeCell[,] grid) {
		if(nextRow >= 0 && nextRow < mazeRows && nextCol >= 0 && nextCol < mazeColumns && !(grid[nextRow,nextCol]).visited)
			return true;
		return false;
	}

	// destory the wall connecting currentCell and nextCell
	void DestroyWall(MazeCell currentCell, MazeCell nextCell) {
		if(currentCell.row < nextCell.row && currentCell.southWall != null) { // moving south
			Destroy(currentCell.southWall);
			return;
		}
		if(currentCell.col < nextCell.col && currentCell.eastWall != null) { // moving east
			Destroy(currentCell.eastWall);
			return;
		}
		if(currentCell.row > nextCell.row && nextCell.southWall != null) { // moving north
			Destroy(nextCell.southWall);
			return;
		}
		if(currentCell.col > nextCell.col && nextCell.eastWall != null) { // moving west
			Destroy(nextCell.eastWall);
			return;
		}
	}

	// gets a list of all valid neighbors for currentCell in the grid
	List<MazeCell> GetNeighbors(MazeCell currentCell, MazeCell[,] grid) {
		int r = currentCell.row;
		int c = currentCell.col;
		List<MazeCell> neighbors = new List<MazeCell>();
		if(isValidCell(r + 1, c, grid)) {
			neighbors.Add(grid[r + 1, c]);
		}
		if(isValidCell(r - 1, c, grid)) {
			neighbors.Add(grid[r - 1, c]);
		}
		if(isValidCell(r, c + 1, grid)) {
			neighbors.Add(grid[r, c + 1]);
		}
		if(isValidCell(r, c - 1, grid)) {
			neighbors.Add(grid[r, c - 1]);
		}
		return neighbors;
	}
}