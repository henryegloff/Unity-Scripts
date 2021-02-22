using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3 : MonoBehaviour
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

    public float movementIntensity = 4;
    public float extraMovementIntensity = 6;
    /* 
    Creates a public variable that will be used to set 
    the default movement intensity
    */

    public Vector3 jumpVector;
    public float jumpForce = 2.0f;
    private bool isGrounded;
    /* 
    Creates a public variable for jump force and a private 
    boolean variable for isGrounded (true / false)
    */

    // Long press
    public float longPress = 2f;
    private float time;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        /* 
        makes our rb variable equal the rigid body component
        of the current object
        */
    }


    void Update()
    {
    	/* 
        Only allow the following actions if the play is grounded
        */

        if (isGrounded)
        {   

        	/* 
            Establish some directions 
            based on the cameraTarget obect's orientation 
            */
            var ForwardDirection = cameraTarget.transform.forward;
            var RightDirection = cameraTarget.transform.right;


            // Move Forwards
            if (Input.GetKey(KeyCode.W)) 
            {
                rb.AddForce (ForwardDirection * movementIntensity);

                /* This example uses velocity rather than force.
                This allows for a more responsive control of the movement
                possibly better suited to first person controls, eg: */

                //rb.velocity = ForwardDirection * movementIntensity;

                //faster if key held down
                time += Time.deltaTime;
                if (time > longPress){
                    rb.AddForce (ForwardDirection * extraMovementIntensity);
                };

            }

            // Move Backwards
            if (Input.GetKey(KeyCode.S))
            {
                // Adding a negative to the direction reverses it
                rb.AddForce (-ForwardDirection * movementIntensity);
                //rb.velocity = -ForwardDirection * movementIntensity;

                time += Time.deltaTime;
                if (time > longPress){
                    rb.AddForce (-ForwardDirection * extraMovementIntensity);
                };

            }

            // Move Rightwards (eg Strafe. *We are using A & D to swivel)
            if (Input.GetKey(KeyCode.E))
            {
               rb.AddForce (RightDirection * movementIntensity);
               //rb.velocity = RightDirection * movementIntensity;
            }

            // Move Leftwards
            if (Input.GetKey(KeyCode.Q))
            {
               rb.AddForce (-RightDirection * movementIntensity);
               //rb.velocity = -RightDirection * movementIntensity;
            }

            
            // jump
            if(Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(jumpVector * jumpForce, ForceMode.Impulse);
                isGrounded = false;
            }

        } // closes the isGrounded condition


        // Shoot Projectile 1
        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Mouse0) ) {
            shootProjectile1();
        }


        // Shoot Projectile 2
        if (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.Mouse1) ) {
            shootProjectile2();
        }


    }


    void OnCollisionStay()
    {
        isGrounded = true;
    }

    void OnCollisionExit()
    {
        isGrounded = false;
    } 





    // Projectiles
    public GameObject Projectile1;
    public GameObject Projectile2;


    public float projectileForwardForce = 500.0f;
    public float projectileUpwardForce = 100.0f;
    public Vector3 projectileTorque;


    void shootProjectile1()
    {
    	var ForwardDirection = cameraTarget.transform.forward;
    	var RightDirection = cameraTarget.transform.right;
    	var UpDirection = cameraTarget.transform.up;

        GameObject clone = Instantiate(Projectile1, transform.position + 1 * ForwardDirection, transform.rotation);

        clone.GetComponent<Rigidbody>().AddForce(ForwardDirection * 1400);
        clone.GetComponent<Rigidbody>().AddForce(UpDirection * 100);
        clone.GetComponent<Rigidbody>().AddTorque(RightDirection * 200);

        Destroy(clone, 1.0f); // Destroy projectile after ... seconds

    }



    void shootProjectile2()
    {
    	var ForwardDirection = cameraTarget.transform.forward;
    	var RightDirection = cameraTarget.transform.right;
    	var UpDirection = cameraTarget.transform.up;

        GameObject clone = Instantiate(Projectile2, transform.position + 1 * ForwardDirection, transform.rotation);

        clone.GetComponent<Rigidbody>().AddForce(ForwardDirection * projectileForwardForce);
        clone.GetComponent<Rigidbody>().AddForce(UpDirection * projectileUpwardForce);
        clone.GetComponent<Rigidbody>().AddTorque(projectileTorque, ForceMode.Impulse);

        Destroy(clone, 1.0f); // Destroy projectile after ... seconds

    }


}
