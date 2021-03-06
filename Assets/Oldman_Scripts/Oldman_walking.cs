﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oldman_walking : MonoBehaviour
{

    public GraphManager gm;
    public float speed;
    private List<Vector3> pathToWalk;
    public Transform[] target;
    public bool ShouldFeed = false;
    public float timer;
    public int LastRANDO;
    Oldman_feeding marty;
    public bool IsSitting = false;
    Animator Anim;
    public GameObject Body;
    void MakeApath()
    {
        Random.InitState((int)Time.time);
        int Rando = Random.Range(0, target.Length);
        //if (Rando >= 3) { Rando = 0; }
        if(Rando == LastRANDO)
        {
            if (LastRANDO == 0) { Rando++; }
            if (LastRANDO == 1) { Rando++; }
            if (LastRANDO == 2) { Rando++; }
            if (LastRANDO == 3) { Rando = 0; }
        }

        if (target != null)
       {
            ShouldFeed = false;
            LastRANDO = Rando;
            pathToWalk = gm.FindPathBetween(transform, target[Rando]);
       }
        
    }
    
    // Use this for initialization
    void Start()
    {
        Anim = Body.GetComponent<Animator>();
        marty = GetComponent<Oldman_feeding>();
        timer = 5;
        MakeApath();
        //if (target != null)
        //    pathToWalk = gm.FindPathBetween(transform, target[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if (pathToWalk.Count == 0)
        {
            IsSitting = true;
            Anim.SetBool("Sit", IsSitting);
            timer -= Time.deltaTime;
            
           
           
            if (timer <= 0)
            {
                //Anim.SetTrigger("Feed");
                MakeApath();
                timer = 5;
                ShouldFeed = true;
                
            }
            
        };
        
        if (ShouldFeed == true)
        {
            marty.DoFeed();

            ShouldFeed = false;
            
        }
        //Fishing stuff

       
      

        //MakeApath()
        if (timer == 5)
        {
            IsSitting = false;
            Anim.SetBool("Sit", IsSitting);
            Vector3 dir = (pathToWalk[0] - transform.position).normalized;

            transform.Translate(dir * speed * Time.deltaTime);
          
            if (Vector3.Distance(pathToWalk[0], transform.position) < .1f)
            {
                pathToWalk.RemoveAt(0);
                Body.transform.LookAt(pathToWalk[0]);
            }

            
        }


        //Anim.SetBool("Sit", IsSitting);

        //transform.position += dir * Time.deltaTime;
    }

    //private void OnValidate()
    //{
    //    if (target != null && gm != null)
    //    {
    //        pathToWalk = gm.FindPathBetween(transform, target[0]);
    //    }
    //    else pathToWalk = null;
    //}
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
