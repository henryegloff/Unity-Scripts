using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerController : MonoBehaviour
{
    /* 
    create a variable called 'rb' that will represent the 
    rigid body of this object
    */
    private Rigidbody rb;

    /* 
    You will need to set the cameraTarget object in Unity. 
    The direction this object is facing will be used to determine
    the direction of forces we will apply to our player
    */
    
    public GameObject cameraTarget;
    
    
    void Start()
    {
        // make our rb variable equal the rigid body component
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {

        var cameraForward = cameraTarget.transform.forward;
        var cameraRight = cameraTarget.transform.right;
        var cameraUp = cameraTarget.transform.up;

        if (Input.GetKey(KeyCode.I)) 
        {
            //rb.AddForce (cameraForward * 1, ForceMode.Impulse);
            //rb.AddForce (cameraForward * 5);
            rb.velocity = cameraForward * 5;
        }

        if (Input.GetKey(KeyCode.K))
        {
            //rb.AddForce (-cameraForward * 5);
            rb.velocity = -cameraForward * 5;
        }

        if (Input.GetKey(KeyCode.J))
        {
           //rb.AddForce (-cameraRight * 5);
           rb.velocity = -cameraRight * 5;
        }

        if (Input.GetKey(KeyCode.L))
        {
           //rb.AddForce (cameraRight * 5);
           rb.velocity = cameraRight * 5;
        }
    }
}
