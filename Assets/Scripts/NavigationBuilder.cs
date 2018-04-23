using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationBuilder : MonoBehaviour {
	private float timer;
	void Update () {
		timer += Time.deltaTime;
		if(timer > 1f) {
			GameObject.Find("Floor").GetComponent<NavMeshSurface>().BuildNavMesh();
			Destroy(this);
		}
	}
}
