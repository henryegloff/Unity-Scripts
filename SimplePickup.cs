using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePickup : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameSystem.score += 1;
            Destroy(gameObject);
        }
    }
}
