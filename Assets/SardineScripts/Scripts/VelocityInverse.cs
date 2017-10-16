using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityInverse : MonoBehaviour {

	void OnCollisionEnter(Collision other)
    {
        var T = other.gameObject.GetComponent<Rigidbody>();
        if(T != null)
        {
            
            T.velocity *= -1;
            T.AddForce(T.velocity * 50);
        }
    }
}
