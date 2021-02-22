using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Rigidbody rb;
    public Transform target;
    public float movementSpeed = 0.25f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.LookAt(target);
        rb.AddForce(transform.forward * movementSpeed);
    }
}
