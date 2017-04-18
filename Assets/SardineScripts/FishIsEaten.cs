using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishIsEaten : MonoBehaviour {

    // Use this for initialization
    

    void OnCollisionEnter(Collision collider)
    {
        if(collider.gameObject.tag == "ItsaFuckingFish" && transform.localScale.x <= 8)
        {
            if(collider.transform.localScale.x >= 15)
            {
                collider.gameObject.GetComponent<SardineController>().Hunger = collider.gameObject.GetComponent<SardineController>().StartHunger;
                Destroy(gameObject);
                
            }
        }
    }

    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
