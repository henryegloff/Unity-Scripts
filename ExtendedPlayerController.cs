using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendedPlayerController : MonoBehaviour
{

	private Rigidbody rb;
    /* 
    Creates a variable called 'rb' that will represent the 
    rigid body of this object.
    */

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

    public GameObject Projectile;
    /*
    Creates a variable for our Projectile.
    In this example this must be a prefab 
    with a rigid body component
    */


    // Variables for jump
    public Vector3 jump;
    public float jumpForce = 2.0f;
    public bool isGrounded;


    void Start()
    {
    	// make our rb variable equal the rigid body component
        rb = GetComponent<Rigidbody>();
    }


 
    void Update()
    {
        if (isGrounded) 
        {

        /* 
        Establish some directions 
        based on the cameraTargets orientation 
        */
        var ForwardDirection = cameraTarget.transform.forward;
        var RightDirection = cameraTarget.transform.right;


        // Move Forwards
        if (Input.GetKey(KeyCode.W)) 
        {
            //rb.AddForce (ForwardDirection * movementIntensity);

            /* This example velocity rather than force.
            This allows for a more responsive control of the movement
            possibly better suited to first person controls, eg: */

            rb.velocity = ForwardDirection * movementIntensity;
        }

        // Move Backwards
        if (Input.GetKey(KeyCode.S))
        {
            // Adding a negative to the direction reverses it
            //rb.AddForce (-ForwardDirection * movementIntensity);
            rb.velocity = -ForwardDirection * movementIntensity;
        }

        // Move Rightwards (eg Strafe. *We are using A & D to swivel)
        if (Input.GetKey(KeyCode.E))
        {
           //rb.AddForce (RightDirection * movementIntensity);
           rb.velocity = RightDirection * movementIntensity;
        }

        // Move Leftwards
        if (Input.GetKey(KeyCode.Q))
        {
           //rb.AddForce (-RightDirection * movementIntensity);
           rb.velocity = -RightDirection * movementIntensity;
        }

        // shoot
        if (Input.GetKeyDown(KeyCode.Space)) {
            shootProjectile();
        }
        /*
        Calls a 'shootProjectile' function
        when the space key is pressed.
        */

        }

        // jump
        if(Input.GetKeyDown(KeyCode.X))
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }


    	
    }

    void shootProjectile()
    {

        var ForwardDirection = cameraTarget.transform.forward;
        var RightDirection = cameraTarget.transform.right;
        var UpDirection = cameraTarget.transform.up;

        GameObject clone = Instantiate(Projectile, transform.position + 1 * ForwardDirection, transform.rotation);

        clone.GetComponent<Rigidbody>().AddForce(ForwardDirection * 600);
        clone.GetComponent<Rigidbody>().AddForce(UpDirection * 200);
        clone.GetComponent<Rigidbody>().AddTorque(RightDirection * 200);

    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    void OnCollisionExit()
    {
        isGrounded = false;
    } 
    

}
