using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    public Transform target;
    public GameObject Projectile;
    public float projectileForwardForce = 1200.0f;
    public float projectileUpwardForce = 0.0f;
    public Vector3 projectileTorque;

    private bool inRange;


    void Start()
    {
        InvokeRepeating("shoot", 0.25f, 0.25f);
    }

    void Update()
    {
        transform.LookAt(target);

        if (Vector3.Distance(transform.position, target.position) <= 11)
        {
            inRange = true;
        } 
        else
        {
            inRange = false;
        }
    }

void shoot()
    {
        if (inRange) {

            var ForwardDirection = transform.forward;
            var RightDirection = transform.right;
            var UpDirection = transform.up;

            GameObject clone = Instantiate(Projectile, transform.position + 1 * ForwardDirection, transform.rotation);

            clone.GetComponent<Rigidbody>().AddForce(ForwardDirection * projectileForwardForce);
            clone.GetComponent<Rigidbody>().AddForce(UpDirection * projectileUpwardForce);
            clone.GetComponent<Rigidbody>().AddTorque(projectileTorque, ForceMode.Impulse);

            Destroy(clone, 2.0f); // Destroy projectile after ... seconds
        }
    }
}
