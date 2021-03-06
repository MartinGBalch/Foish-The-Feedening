﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbPathWalker : MonoBehaviour {

    public GraphManager gm;
    public float speed;
    private List<Vector3> pathToWalk;
    public Transform target;
	// Use this for initialization
	void Start ()
    {
        if (target != null)
            pathToWalk = gm.FindPathBetween(transform, target);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (pathToWalk.Count == 0) return;
       
          

            Vector3 dir = (pathToWalk[0] - transform.position).normalized;

            transform.Translate(dir * speed * Time.deltaTime);

        if (Vector3.Distance(pathToWalk[0], transform.position) < .1f)
        {
            pathToWalk.RemoveAt(0);
        }

        //transform.position += dir * Time.deltaTime;
    }

    private void OnValidate()
    {
        if (target != null && gm != null)
        {
            pathToWalk = gm.FindPathBetween(transform, target);
        }
        else pathToWalk = null;
    }
    private void OnDrawGizmos()
    {
        if(pathToWalk != null)
        foreach(var t in pathToWalk)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(t, .5f);
        }
    }

}
