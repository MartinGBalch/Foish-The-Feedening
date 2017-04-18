using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour {

    public GameObject SpawnBaby;
    public float radius;
    public float spawnCount;
    public float Timer;
    private float StartTime;
    private float StartCount;
	// Use this for initialization
	void Start ()
    {
        StartTime = Timer;
        StartCount = spawnCount;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Timer -= Time.deltaTime;

		if(Timer <= 0)
        {
            Timer = StartTime;
            for(int i = 0; i < spawnCount; ++i)
            {
                float X = Random.Range(-radius, radius);
                float Z = Random.Range(-radius, radius);
                SpawnBaby.transform.position = new Vector3(X, transform.position.y, Z);
                Instantiate(SpawnBaby);
            }
        }
	}
}
