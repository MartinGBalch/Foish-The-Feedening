using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oldman_feeding : MonoBehaviour
{
    public GameObject SpawnFood;
    public float radius;
    public float spawnCount;
    public GameObject Body;
    private float StartTime;
    private float StartCount;
    public GameObject[] Feeder;
    Vector3 kobey = Vector3.zero;
    //Animator Anim;
    public void Start()
    {
        //Anim = Body.GetComponent<Animator>();
        StartCount = spawnCount;
    }
    public void DoFeed()
    {
        
        kobey = transform.forward * 10;

        spawnCount = StartCount;

        for (int i = 0; i < spawnCount; ++i)
        {
            float X = Random.Range(-radius, radius);
            float Z = Random.Range(-radius, radius);
            SpawnFood.transform.position = new Vector3(kobey.x + X, kobey.y +2, kobey.z + Z);
            Instantiate(SpawnFood);
        }
    }
    //void OnCollisionEnter(Collision collider)
    //{
    //    if (collider.gameObject.tag == "feed_plat")
    //    {
    //        kobey = transform.forward * 10;

    //        spawnCount = StartCount;

    //        for (int i = 0; i < spawnCount; ++i)
    //        {
    //            float X = Random.Range(-radius, radius);
    //            float Z = Random.Range(-radius, radius);
    //            SpawnFood.transform.position = new Vector3(kobey.x + X, kobey.y, kobey.z + Z);
    //            Instantiate(SpawnFood);
    //        }
    //    }
    //}
}
