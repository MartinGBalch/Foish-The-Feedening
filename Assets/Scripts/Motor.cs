using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour
{
    private Rigidbody rb;

    public float walkSpeed;
    public float walkMaxSpeed;
    public float walkRestRate;

    private float walkInput;

    public void doWalk(float input01) { walkInput += input01;   /*walkInput = Mathf.Clamp(walkInput, -1, 1); */}


	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //var currentSpeed = Vector3.Project(rb.velocity, Vector3.forward);

        var desiredVelocity = Vector3.forward * walkMaxSpeed;
        var steer = desiredVelocity - rb.velocity;

        rb.AddForce(steer.normalized * walkSpeed *walkInput);
        walkInput = 0;
	}
}
