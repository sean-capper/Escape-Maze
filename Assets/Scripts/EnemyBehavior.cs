using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
    public int health;
    public float moveSpeed = 3.0f;
    public float armRange = 2.0f;
    public GameObject player;
    
	
	// Update is called once per frame
	void Update () {
        //moving the enmey forward
        Vector3 movement = transform.forward * moveSpeed * Time.deltaTime;

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        //
        if((transform.position - player.transform.position).magnitude > armRange)
        {
            transform.Translate(movement, Space.World);

            //doing a spherecast to detect the player headset
            if(Physics.SphereCast(ray, 1.50f, out hit))
            {
                //returning the gameobject hit by the spherecast
                GameObject objectHit = hit.transform.gameObject;

                if (objectHit.CompareTag("Player"))
                {
                    Vector3 targetLocation = new Vector3(objectHit.transform.position.x,
                                                this.transform.position.y,
                                                objectHit.transform.position.z);
                    this.transform.LookAt(targetLocation);
                    Debug.Log("Player found!");
                }
                if(hit.distance <armRange && !objectHit.CompareTag("Player"))
                {
                    Debug.Log("About to collide with:\n" + objectHit.ToString() + "\nRotationg!");
                    float rotateAngle = Random.Range(-100, 100);
                    transform.Rotate(0, rotateAngle, 0);
                }
            }
        }
	}
}
