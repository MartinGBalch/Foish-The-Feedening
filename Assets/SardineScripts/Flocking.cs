using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocking : MonoBehaviour
{
    public GameObject Prey;
    public float radius;
    public float speed;
    Rigidbody rbz;
    Vector3 Cforce = Vector3.zero;
    Vector3 Aforce = Vector3.zero;
    Vector3 Sforce = Vector3.zero;
    Vector3 Wander = Vector3.zero;
   public bool IsEvading = false;
   // public GameObject Lead;

    public float jitter = 1;
    public float distance = 1;


    void Start ()
    {
        rbz = GetComponent<Rigidbody>();	

	}
     void DoWander()
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


        Vector3 steeringForce = desiredVelocity - rbz.velocity;
        Wander = steeringForce;

       // transform.forward = new Vector3(rbz.velocity.x, 0, rbz.velocity.z);
    }
    void DoEvade()
    {
        Vector3 target = Prey.transform.position + Prey.GetComponent<Rigidbody>().velocity;
        Vector3 dir = -(target - transform.position).normalized;
        Vector3 desiredVelocity = dir * speed;
        Vector3 steeringForce = desiredVelocity - rbz.velocity;




        rbz.AddForce(steeringForce.normalized * 2);


        Vector3 head = rbz.velocity;
        head.y = 0;
        transform.LookAt(transform.position + head, Vector3.up);

        if (Vector3.Distance(transform.position, Prey.transform.position) <= radius) { IsEvading = false; }

    }
    void flocking()
    {
        Vector3 Ctarget = Vector3.zero;
        Vector3 aDesire = Vector3.zero;
        Vector3 sSum = Vector3.zero;
        
        Collider[] Hood = Physics.OverlapSphere(transform.position, radius);
        
        foreach( Collider T in Hood)
        {
           var Q = T.GetComponent<SardineController>();
            if(Q != null)
            {
                if (Q.BigFoish == true)
                {
                    Prey = Q.gameObject;
                    IsEvading = true;
                }
               
            }
            else if (T.GetComponent<Rigidbody>() == true )
            {
               
                
                Rigidbody rb = T.GetComponent<Rigidbody>();
                Ctarget += T.transform.position;
                aDesire += rb.velocity;
                sSum += (transform.position - T.transform.position) * (radius - Vector3.Distance(transform.position, T.transform.position))/* / radius*/;
            }
            
        }
        Ctarget /= Hood.Length;
        aDesire /= Hood.Length;
        sSum /= Hood.Length;

        Cforce = (Ctarget - transform.position) - rbz.velocity;
        Aforce = aDesire - rbz.velocity;
        Sforce = sSum - rbz.velocity;
    }
    void flockingForce()
    {
       // var X = Lead.GetComponent<Rigidbody>().velocity;
        //rbz.AddForce(Wander * speed);
        rbz.AddForce(Cforce * speed);
        rbz.AddForce(Aforce * speed);
        rbz.AddForce(Sforce * speed);

        Vector3 head = rbz.velocity;
        head.y = 0;
        transform.LookAt(transform.position + head, Vector3.up);
        //transform.LookAt(Lead.transform);
    }
	
	void Update ()
    {
        if (IsEvading == false) { flocking();  }
        if (IsEvading == true && Prey != null)
        { DoEvade(); }
    }
    void FixedUpdate()
    {
        if (IsEvading == false) { flockingForce(); }
        
    }
}
