using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFlee : MonoBehaviour {

    public GameObject target;
    public float speed = 1;
    private Vector3 velocity;
    Rigidbody rb;
    // Use this for initialization

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 dir = -(target.transform.position - transform.position).normalized;
        Vector3 desiredVelocity = dir * speed;


        Vector3 steeringForce = desiredVelocity - rb.velocity;
        rb.AddForce(steeringForce);
        //velocity += steeringForce * Time.deltaTime;
        //transform.position += velocity * Time.deltaTime;
        
       

        Vector3 head = rb.velocity;
        head.y = 0;
        transform.LookAt(transform.position + head, Vector3.up);
        //transform.LookAt((target.transform.forward));

        //if (Vector3.Distance(target.transform.position, transform.position) < 2)
        //{
        //    Destroy(target);
        //}
    }



}


