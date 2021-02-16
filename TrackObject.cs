using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackObject : MonoBehaviour
{

    private Rigidbody rb;
    public Transform target; // The object that will be tracked
	
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.LookAt(target);
        //rb.AddForce(transform.forward * 1); // Apply forwards force to move the object towards the target
    }
}
