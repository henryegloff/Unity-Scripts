using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerExtended : MonoBehaviour
{

    private Rigidbody rb;
    public GameObject myProjectile; // must be prefab with a rigid body
    public GameObject cameraParent;



    // Variables for jump
    public Vector3 jump;
    public float jumpForce = 2.0f;
    public bool isGrounded;





    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isGrounded)
        {



            //var cameraForward = Camera.main.transform.forward;
            //var cameraRight = Camera.main.transform.right;
            var cameraForward = cameraParent.transform.forward;
            var cameraRight = cameraParent.transform.right;
            var cameraUp = cameraParent.transform.up;

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

            if (Input.GetKey(KeyCode.X))
            {
                rb.AddForce(cameraUp * 11);
                //rb.velocity = cameraRight * 5;
            }

            // shoot
            if (Input.GetKeyDown(KeyCode.Space))
            {
                shootProjectile();
            }


            // jump
            if (Input.GetKeyDown(KeyCode.X))
            {
                rb.AddForce(jump * jumpForce, ForceMode.Impulse);
                isGrounded = false;
            }




        }





    }

    void OnCollisionStay()
    {
        isGrounded = true;
        Debug.Log("player grounded");
    }

    void OnCollisionExit()
    {
        isGrounded = false;
        Debug.Log("player jumping");
    }




    void shootProjectile()
    {

        var cameraForward = Camera.main.transform.forward;
        var cameraRight = Camera.main.transform.right;
        var cameraUp = Camera.main.transform.up;

        GameObject clone = Instantiate(myProjectile, transform.position + 1 * cameraForward, transform.rotation);

        clone.GetComponent<Rigidbody>().AddForce(cameraForward * 600);
        clone.GetComponent<Rigidbody>().AddForce(cameraUp * 200);
        clone.GetComponent<Rigidbody>().AddTorque(cameraRight * 200);

        Destroy(clone, 5.0f);
    }









}
