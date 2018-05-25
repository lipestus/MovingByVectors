using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsNavigation : MonoBehaviour {

    public GameObject[] waypoints;
    int currentWaypoint = 0;
    private Animator anim;

    float speed = 1.5f;
    float accuracy = 1f;
    float rotationSpeed = 1f;

	// Use this for initialization
	void Start () {
        waypoints = GameObject.FindGameObjectsWithTag("waypoints");
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (waypoints.Length == 0)
            return;

        Vector3 lookAtGoal = new Vector3(waypoints[currentWaypoint].transform.position.x, this.transform.position.y, waypoints[currentWaypoint].transform.position.z);
        Vector3 direction = lookAtGoal - this.transform.position; // get magnitude
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);
        Debug.DrawRay(this.transform.position, direction, Color.green);

        if(direction.magnitude < accuracy)
        {
            currentWaypoint++;
            if(currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
        }

        anim.SetFloat("speed", speed);
        this.transform.Translate(0, 0, speed * Time.deltaTime);
	}
}
