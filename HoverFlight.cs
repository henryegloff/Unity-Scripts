using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverFlight : MonoBehaviour
{

    private Rigidbody rb;
    public float forwardThrust = 88;
    public float verticalThrust = 88;
    public float horizontalThrust = 88;
    public float steeringIntensity = 2;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        var upDirection = rb.transform.up;
        var forwardDirection = rb.transform.forward;
        var RightDirection = rb.transform.right;

        // Forwards
        if (Input.GetKey(KeyCode.W) )
        {
        	rb.AddForce(forwardDirection * forwardThrust);
        }

        // Backwards
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-forwardDirection * forwardThrust);
        }

        // Strafe Right
        if (Input.GetKey(KeyCode.E))
        {
            rb.AddForce(RightDirection * forwardThrust);
        }

        // Strafe Left
        if (Input.GetKey(KeyCode.Q))
        {
            rb.AddForce(-RightDirection * forwardThrust);
        }

        // Upwards
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(upDirection * verticalThrust);
        }

        // Turn Left
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * steeringIntensity * 100, Space.World);
        }
        
        // Turn Right
        if (Input.GetKey(KeyCode.D))
        {
        	transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * steeringIntensity * 100, Space.World);
        }

        // Downwards force 
        if (Input.GetKey(KeyCode.X))
        {
            rb.AddForce(-upDirection * verticalThrust);
        }
      

    }
}
