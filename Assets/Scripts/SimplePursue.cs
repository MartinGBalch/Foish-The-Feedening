using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePursue : MonoBehaviour {

    public GameObject Prey;
    public float speed = 1;
    private Vector3 velocity;
    public float ChaseAngle;
    Rigidbody rb;
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        var thingy = Vector3.Distance(Prey.transform.position, transform.position)/
        Prey.GetComponent<Rigidbody>().velocity.magnitude;

        Vector3 target = Prey.transform.position + (thingy * Prey.GetComponent<Rigidbody>().velocity);

        //target += Prey.transform.forward * ChaseAngle;

        //if (Vector3.Distance(target, transform.position) < 3)
        //{
        //    target = Prey.transform.position;
        //}
        // else
        //{
        //    target += Prey.transform.forward * ChaseAngle;
        //}
        // print(target);

        Vector3 dir = (target - transform.position).normalized;
        Vector3 desiredVelocity = dir * speed;


        Vector3 steeringForce = desiredVelocity - rb.velocity;
        rb.AddForce(steeringForce);
        Vector3 head = rb.velocity;
        head.y = 0;
        transform.LookAt(transform.position + head, Vector3.up);
    }
}
