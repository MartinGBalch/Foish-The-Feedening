using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : MonoBehaviour {
    public GraphManager gm;
    private List<Vector3> pathToWalk;
    public GameObject target;
    public float speed = 1;
    private Vector3 velocity;
    Rigidbody rb;
    // Use this for initialization

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
		if(gm != null)
        {
            pathToWalk = gm.FindPathBetween(transform, target.transform);
           
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (pathToWalk.Count > 0 && pathToWalk != null)
        {
            Vector3 dir = (pathToWalk[0] - transform.position).normalized;
            Vector3 desiredVelocity = dir * speed;

            
            Vector3 steeringForce = desiredVelocity - rb.velocity;
            rb.AddForce(steeringForce);
            //velocity += steeringForce * Time.deltaTime;
            //transform.position += velocity * Time.deltaTime;

            transform.LookAt(transform.position + rb.velocity.normalized, Vector3.forward);
            if (Vector3.Distance(pathToWalk[0], transform.position) < .25f)
            {
                pathToWalk.RemoveAt(0);
            }
        }

       

    }

    private void OnDrawGizmos()
    {
        if (pathToWalk != null)
            foreach (var t in pathToWalk)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawWireSphere(t, .5f);
            }
    }
}
