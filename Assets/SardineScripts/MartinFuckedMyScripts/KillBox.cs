using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour {

    public GameObject T;


	// Use this for initialization
	void Start () {
		
	}
	

    void OnTriggerExit(Collider other)
    {
        float x = Random.Range(-5, 5);
        float z = Random.Range(-5, 5);
        other.transform.position = new Vector3(T.transform.position.x + x, T.transform.position.y, T.transform.position.z + z);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
