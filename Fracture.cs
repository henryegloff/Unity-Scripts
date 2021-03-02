using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fracture : MonoBehaviour
{
    
    public GameObject fracturedPrefab;
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile3")
        {
            GameObject clone = Instantiate(fracturedPrefab, transform.position, transform.rotation); 
            clone.transform.localScale = transform.localScale;
            Destroy(gameObject);
        }
    }
}

