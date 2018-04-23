using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {

	[SerializeField] GameObject Monster;
	[SerializeField] int numMonsters;
	private Maze maze;
	private float timer;
	bool Done = false;
	int currentMonsters = 0;
	// Use this for initialization
	void Start () {
		maze = GameObject.Find("_Maze").GetComponent<Maze>();
		
	}

	void Update() {
		timer += Time.deltaTime;
		if(timer > 1f) {
			if(!Done && currentMonsters < numMonsters) {
				Instantiate(Monster, maze.GetRandomCell().position, Quaternion.identity);
				currentMonsters++;
			} else {
				Done = true;
			}
		}
	}
}
