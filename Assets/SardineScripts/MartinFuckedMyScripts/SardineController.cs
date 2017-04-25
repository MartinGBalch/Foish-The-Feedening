using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SardineController : MonoBehaviour {

    private SimpleWander wander;
    private PathlessSeeking seeker;
   // public GameObject SpawnBabyz;
    public int SpawnCountz;

    public float Hunger;
    public float StartHunger;

    public float radius;

    public bool Seeking = false; // Wander-True    Seeker-False
    public bool BigFoish = false;
    bool Dead = false;


   
    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Food" && seeker.target == null && BigFoish == false)
        {
            seeker.target = other.gameObject;
            Seeking = true;
        
        }

        //if(other == null)
        //{
        //    Debug.Log("other");
        //}
        //else if (seeker == null)
        //{
        //    Debug.Log("seeker");
        //}

        if (other.tag == "ItsaFuckingFish" && seeker.target == null && BigFoish == true)
        {
            if(other.transform.localScale.x <= 8)
            {
                seeker.target = other.gameObject;
                Seeking = true;
                
            }
            
        }
    }
    void OnCollisionEnter(Collision collider)
    {
        if(collider.gameObject.tag == "ItsaFuckingFish")
        {
            if (Seeking == false)
            {
                wander.target *= -1;
            }
            if (Seeking == true)
            {
                //seeker.rb.velocity *= 1;
            }
        }
     
    }

	void Awake ()
    {
        transform.localScale = new Vector3(5, 5, 5);
        Seeking = false;
        BigFoish = false;
        Dead = false;
        Hunger = 10;
        StartHunger = Hunger;
        wander = GetComponent<SimpleWander>();
        seeker = GetComponent<PathlessSeeking>();
	}
	//void SeperationForce()
 //   {
 //       Vector3 sSum = Vector3.zero;
 //       int hood = 0;
 //       Collider[] Hood = Physics.OverlapSphere(transform.position, radius);
 //       foreach (Collider T in Hood)
 //       {
 //           if (T.GetComponent<Rigidbody>() == true || T.tag == "Wall")
 //           {
 //               hood++;
 //               Rigidbody rb = T.GetComponent<Rigidbody>();
                
 //               sSum += (transform.position - T.transform.position) / radius;// * (radius - Vector3.Distance(transform.position, T.transform.position)) / radius;
 //           }
 //       }
 //       sSum /= hood;

 //      Vector3 Sforce = sSum.normalized * wander.speed - wander.rb.velocity;
 //       wander.rb.AddForce(Sforce);
 //   }
	// Update is called once per frame
	void Update ()
    {
        
       
        if (seeker.target != null && seeker != null)
        {
            if (seeker.target.transform.localScale.x > 8) { seeker.target = null; }
            if (seeker.target.tag == "Food" && transform.localScale.x >= 15) { seeker.target = null; }

        }
        if (seeker.target == null)
        {
            Seeking = false;
        }
        if (Seeking == true && seeker.target != null)
        {
           
            seeker.DoSeek();
            wander.DoObstacle();
           // SeperationForce();
        }
       
        if(Seeking == false)
        {
            wander.DoWander();
           // wander.DoNewWander();
            wander.DoObstacle();
          //  SeperationForce();
        }
        

        
       

        if (transform.localScale.x >= 15 && BigFoish == false) { BigFoish = true; }
        if(BigFoish == true)
        {
            gameObject.name = "BigFoish";
            Hunger -= Time.deltaTime;
            if(Hunger <= 0)
            {
                Dead = true;
            }
            
        }
        if (Dead == true)
        {
            for (int i = 0; i < SpawnCountz; ++i)
            {
                //SpawnBabyz.GetComponent<SardineController>().Start();
                float X = Random.Range(-3, 3);
                float Z = Random.Range(-3, 3);
                //SpawnBabyz.transform.position = new Vector3(transform.position.x + X, transform.position.y, transform.position.z + Z);
                var t = Instantiate(gameObject);
                t.transform.position = new Vector3(transform.position.x + X, transform.position.y, transform.position.z + Z);
                //t.GetComponent<SardineController>().Start();
                //t.GetComponent<SardineController>().SpawnBabyz = SpawnBabyz;
            }
            Destroy(gameObject);
        }
    }
}
