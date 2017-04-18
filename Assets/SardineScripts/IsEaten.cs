using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsEaten : MonoBehaviour {

    public float Growth;
    private Vector3 growth;

	void OnCollisionEnter(Collision collider)
    {
        if(collider.gameObject.tag == "ItsaFuckingFish" && collider.gameObject.transform.localScale.x < 15)
        {
            collider.transform.localScale += growth;
            //collider.gameObject.GetComponent<SardineController>().Seeking = false;
            Destroy(gameObject);
        }
    }
    
    
    
    
    // Use this for initialization
	void Start ()
    {
        growth = new Vector3(Growth, Growth, Growth);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
