using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackObjectPosition : MonoBehaviour
{

    private Rigidbody rb;
    public Transform target; // The object that will be tracked
    public float movementSpeed = 0.25f;
	
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.LookAt(target);
        //rb.AddForce(transform.forward * movementSpeed); // Moves the object towards the target
    }
}
