using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLocal : MonoBehaviour {

    public Transform goal;
    private float speed = 0f;
    private float accuracy = 2f;
    private float rotationSpeed = 2f;
    private float dist;
    float delay = 1.0f;

    private Animator anim;

	// Use this for initialization
	void Awake () {
        anim = GetComponent<Animator>();
        
    }
	
	// Update is called once per frame
	void LateUpdate () {

        anim.SetFloat("speed", speed);
        Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);
        dist = Vector3.Distance(transform.position, lookAtGoal);
        Vector3 direction = lookAtGoal - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);
        Debug.DrawRay(this.transform.position, direction, Color.red);

        if (dist > accuracy)
        {
            StartCoroutine(DelayToWalk(delay));
        }

        else
        {
            speed = 0;
        }

        print(dist);
    }

    public IEnumerator DelayToWalk(float delay)
    {
        yield return new WaitForSeconds(delay);
        speed = 1f;
        if(speed != 0)
            this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
