using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player; 
    
    /* 
    You will need to set the player object in Unity. 
    The object that has this script attached to it, ie the 'Camera Target' object
    will then align itself with whatever object is set as the 'Player'
    */

    void Update()
    {
        
        // Align to player position
        transform.position = player.transform.position;
        
        // Rotate left
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * 100, Space.World);
        }
        
        // Rotate right
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * 100, Space.World);
        }
    }
}
