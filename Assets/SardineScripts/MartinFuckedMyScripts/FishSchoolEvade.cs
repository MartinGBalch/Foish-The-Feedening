using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSchoolEvade : MonoBehaviour {

    public GameObject Prey;
    // Use this for initialization
    public float speed = 1;
    private Vector3 velocity;
    //public float ChaseAngle;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {






        Vector3 target = Prey.transform.position + Prey.GetComponent<Rigidbody>().velocity;
        Vector3 dir = -(target - transform.position).normalized;
        Vector3 desiredVelocity = dir * speed;
        Vector3 steeringForce = desiredVelocity - rb.velocity;




        rb.AddForce(steeringForce.normalized * 2);


        Vector3 head = rb.velocity;
        head.y = 0;
        transform.LookAt(transform.position + head, Vector3.up);
    }
}
