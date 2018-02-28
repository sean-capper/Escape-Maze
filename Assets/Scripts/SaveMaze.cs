using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Maze))]
public class SaveMaze : Editor {
	public override void OnInspectorGUI() {
		DrawDefaultInspector();
		Maze save = (Maze)target;
		if(GUILayout.Button("Save Maze")) {
			GameObject temp = GameObject.Find("_Maze");
			Destroy(temp.GetComponent<Maze>());
			PrefabUtility.CreatePrefab("Assets/Prefabs/Mazes/maze_"+Random.Range(0,100)+".prefab", temp);
		}
	}
	
}
