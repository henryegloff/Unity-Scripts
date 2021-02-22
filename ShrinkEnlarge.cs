using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkEnlarge : MonoBehaviour
{

    private Rigidbody rb;
   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
           gameObject.transform.localScale -= new Vector3(0.3f, 0.3f, 0.3f);
        }

        if (collision.gameObject.tag == "Projectile2")
        {
           gameObject.transform.localScale += new Vector3(1.3f, 1.3f, 1.3f);	
        }

    }
}
