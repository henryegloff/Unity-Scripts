using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerController : MonoBehaviour
{

	/* 
    Create a variable called 'rb' that will represent the 
    rigid body of this object.
    */
    private Rigidbody rb;
    
    // Create a public variable for the cameraTarget object
    public GameObject cameraTarget;
    /* 
    You will need to set the cameraTarget object in Unity. 
    The direction this object is facing will be used to determine
    the direction of forces we will apply to our player.
    */

    public float movementIntensity;
    /* 
    Creates a public variable that will be used to set 
    the movement intensity (from within Unity)
    */


    void Start()
    {
    	// make our rb variable equal the rigid body component
        rb = GetComponent<Rigidbody>();
    }

 
    void Update()
    {
    	/* 
    	Establish some directions 
    	based on the cameraTarget object's orientation 
    	*/
        var ForwardDirection = cameraTarget.transform.forward;
        var RightDirection = cameraTarget.transform.right;


        // Move Forwards
        if (Input.GetKey(KeyCode.W)) 
        {
            rb.AddForce (ForwardDirection * movementIntensity);

            /* You may want to try using velocity rather than force.
            This allows for a more responsive control of the movement
            possibly better suited to first person controls, eg: */

            //rb.velocity = ForwardDirection * movementIntensity;
        }

        // Move Backwards
        if (Input.GetKey(KeyCode.S))
        {
        	// Adding a negative to the direction reverses it
            rb.AddForce (-ForwardDirection * movementIntensity);
        }

        // Move Rightwards (eg Strafe. *We are using A & D to swivel)
        if (Input.GetKey(KeyCode.E))
        {
           rb.AddForce (RightDirection * movementIntensity);
        }

        // Move Leftwards
        if (Input.GetKey(KeyCode.Q))
        {
           rb.AddForce (-RightDirection * movementIntensity);
        }
    }
}
