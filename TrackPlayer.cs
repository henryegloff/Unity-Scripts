using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{

    private Rigidbody rb;
    public Transform target;
	
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.LookAt(target);
        rb.AddForce(transform.forward * 1);
    }
}
