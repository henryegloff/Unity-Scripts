using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTorque : MonoBehaviour
{

	/* 

	This script adds torque to an rigid body object that 
	keeps updating until it reaches terminal velocity.

	It is useful for physical objects in the game
	that will interact with other objects.

	It seems to work best when the Position and Rotation
	is frozen on all axis' except the torque axis,
	and when the object is not right on a ground plane.

	Also for the object to have a high mass, eg Cube:
	Scale = 10, 2, 2
	Torque Y = 22
	
    */

    private Rigidbody rb;

    public Vector3 TorqueIntensity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Remove Forcemode for slow speed up
        rb.AddTorque((TorqueIntensity), ForceMode.Impulse);
    }
}
