using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Transform target;
    private Vector3 followOffset;
	private void Start()
    {
        followOffset = transform.position - target.position;
    }
	void Update ()
    {
        transform.position = target.position + followOffset;
	}
}
