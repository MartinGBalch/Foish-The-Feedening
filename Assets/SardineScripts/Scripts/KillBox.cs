using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KillBox : MonoBehaviour {

    public GameObject T;

    void OnTriggerExit(Collider other)
    {
        float x = Random.Range(-20, 20);
        float z = Random.Range(-20, 20);
        other.transform.position = new Vector3(T.transform.position.x + x, 4, T.transform.position.z + z);
    }

	// Update is called once per frame
	void Update () {}

    public void Observe()
    {
        SceneManager.LoadScene("FoishTheFeedening");
    }
}
