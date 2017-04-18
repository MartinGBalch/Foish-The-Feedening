using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject Foish;
    public float radius;
    public float spawnCount;
    
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < spawnCount; ++i)
        {
            float X = Random.Range(-radius, radius);
            float Z = Random.Range(-radius, radius);
            Foish.transform.position = new Vector3(X, transform.position.y, Z);
            Instantiate(Foish);
        }
    }

    // Update is called once per frame
    void Update()
    {


    }
    }

