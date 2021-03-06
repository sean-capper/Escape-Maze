﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

	[SerializeField] private float FieldOfView;
	[SerializeField] private float SightDistance;
	private NavMeshAgent agent;
	private Transform player;
	private Transform targetPos;
	private Maze maze;
	private bool wandering;

	void Start() {
		agent = GetComponent<NavMeshAgent>();
		player = GameObject.FindWithTag("Player").transform;
		maze = GameObject.Find("_Maze").GetComponent<Maze>();
	}


	void Update ()
    {
		if(player == null) {
			player = GameObject.FindWithTag("Player").transform;
			maze = GameObject.Find("_Maze").GetComponent<Maze>();
			Wander();
		}

        if(CanSeePlayer()) {
			agent.transform.LookAt(targetPos);
			targetPos = player.transform;
			wandering = false;
		}
		else if (wandering) {
			if((transform.position.z == targetPos.position.z) && (transform.position.z == targetPos.position.z)) {
				wandering = false;
			}
		}
		else {
			Wander();
		}

		if(targetPos != null) {
			agent.destination = targetPos.position;
		}
    }

    private bool CanSeePlayer()
    {
        RaycastHit hit;
        Vector3 direciton = player.position - transform.position;

        if ((Vector3.Angle(direciton, transform.forward) <= FieldOfView * 0.5f)) {
			// Debug.DrawRay(transform.position, direciton, Color.red, 2f);
            if (Physics.Raycast(transform.position, direciton, out hit, SightDistance))
            {
                return (hit.transform.CompareTag("Player"));
            }
			
        }

        return false;
    }

	private void Wander() {
		targetPos = maze.GetRandomCell();
		wandering = true;
	}
}