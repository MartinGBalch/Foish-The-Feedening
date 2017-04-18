using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public GraphManager gm;
    private List<Vector3> pathToWalk;
    public GameObject target;
    public float speed = 1;
    public float radius = 1;
    private Vector3 velocity;
    Rigidbody rb;



    Vector3 avoidance(Vector3 position, Vector3 direction, float distance, float radius, float speed)
    {
        RaycastHit info;
        bool res = Physics.SphereCast(position, radius, direction, out info, distance);

        var desiredVelocity = info.normal * speed / Mathf.Max(info.distance,1);
        return res ? desiredVelocity : Vector3.zero;

    }

    Vector3 seek(Vector3 position, Vector3 target, float speed)
    {
        Vector3 dir = (target - position).normalized;
        var dist = Vector3.Distance(target, position);
        Vector3 desiredVelocity = dir * speed;

        //Vector3 steeringForce = desiredVelocity - velocity;
        return desiredVelocity;
    }


    // Return desired velocity
    Vector3 arrival(Vector3 position,Vector3 target,float speed, float radius)
    {
        var desiredVelocity = seek(position, target, speed);
        var dist = Vector3.Distance(target, position);
        

        if(dist < radius)
        {
           desiredVelocity *=  dist / radius;
        }

        
        return desiredVelocity;
    }


	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        if (gm != null)
        {
            pathToWalk = gm.FindPathBetween(transform, target.transform);

        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        var desired =  arrival(transform.position, pathToWalk[0], speed, radius) + .25f * avoidance(transform.position, transform.forward,2f,2,speed);
        var force = desired - rb.velocity;
        rb.AddForce(force);

        Vector3 head = rb.velocity;
        head.y = 0;
        transform.LookAt(transform.position + head, Vector3.up);

        if (Vector3.Distance(transform.position, pathToWalk[0]) < 1)
        {
            pathToWalk.RemoveAt(0);
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
