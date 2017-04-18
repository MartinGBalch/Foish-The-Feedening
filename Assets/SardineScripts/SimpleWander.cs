﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWander : MonoBehaviour {

    
    public float speed = 1;
    public float radius = 1;
    public float jitter = 1;
    public float distance = 1;
  
    Rigidbody rb;
    // Use this for initialization
    void Start ()
    {
        
        rb = GetComponent<Rigidbody>();
    }
	public void DoWander()
    {
        
        Vector3 target = Vector3.zero;
        target = Random.insideUnitCircle.normalized * radius;

        target = (Vector2)target + Random.insideUnitCircle * jitter;
        target = target.normalized * radius;


        target.z = target.y;
        target.y = 0.0f;
        target += transform.position;
        target += transform.forward * distance;




        Vector3 dir = (target - transform.position).normalized;
        Vector3 desiredVelocity = dir * speed;


        Vector3 steeringForce = desiredVelocity - rb.velocity;
        rb.AddForce(steeringForce);

        transform.forward = new Vector3(rb.velocity.x, 0, rb.velocity.z);
    }




	// Update is called once per frame
	//void Update ()
 //   {
 //       Vector3 target = Vector3.zero;
 //       target = Random.insideUnitCircle.normalized * radius;

 //       target = (Vector2)target + Random.insideUnitCircle * jitter;
 //       target = target.normalized * radius;


 //       target.z = target.y;
 //       target.y = 0.0f;
 //       target += transform.position;

 //       //Vector3 Rando = new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2));
 //       //Rando *= jitter;

 //       //target += Rando;

 //       //Xrand = Random.Range(-4, 4);
 //       //Zrand = Random.Range(-4, 4);
 //       //Vector3 RandDist = new Vector3(Xrand, 0, Zrand);
 //       //target += RandDist;

 //       target += transform.forward* distance;




 //       Vector3 dir = (target - transform.position).normalized;
 //       Vector3 desiredVelocity = dir * speed;


 //       Vector3 steeringForce = desiredVelocity - rb.velocity;
 //       rb.AddForce(steeringForce);

 //       transform.forward = new Vector3(rb.velocity.x, 0, rb.velocity.z);
 //       //Vector3 head = rb.velocity;
 //       //head.y = 0;
 //       //transform.LookAt(transform.position + head, Vector3.up);
 //   }
}
