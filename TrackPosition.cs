using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPosition : MonoBehaviour
{
    /* 
    This script updates the position of the current object
    to the position of the target object
    */

    public Transform target; // Set this in the inspector

    void Update()
    {
        transform.position = target.transform.position;
    }
}
