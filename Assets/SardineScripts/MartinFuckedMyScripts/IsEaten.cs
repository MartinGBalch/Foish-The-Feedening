using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsEaten : MonoBehaviour {

    public float Growth;
    private Vector3 growth;
    float timer = 30;
	void OnCollisionEnter(Collision collider)
    {
        if(collider.gameObject.tag == "ItsaFuckingFish" && collider.gameObject.transform.localScale.x < 15)
        {
           
        }
    }
    
    
    
    
    // Use this for initialization
	void Start ()
    {
        growth = new Vector3(Growth, Growth, Growth);
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer -= Time.deltaTime;
		if(timer <= 0)
        {
            Destroy(gameObject);
        }
	}
}
