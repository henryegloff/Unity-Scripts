using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleCannon : MonoBehaviour
{

    void Update () 
    {
 
    	transform.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
     	Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
     	float midPoint = (transform.position - Camera.main.transform.position).magnitude * 0.5f;
     	transform.LookAt(mouseRay.origin + mouseRay.direction * midPoint);

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            shoot();   
        }

    }

    // Projectile
    public GameObject Projectile;
    public float projectileForwardForce = 5000.0f;
    public float projectileLifeSpan = 2.0f;
    public Vector3 projectileTorque;

    void shoot()
    {
        var ForwardDirection = -transform.forward;
        GameObject clone = Instantiate(Projectile, transform.position + 3 * ForwardDirection, transform.rotation);
        clone.GetComponent<Rigidbody>().AddForce(ForwardDirection * projectileForwardForce);
        Destroy(clone, projectileLifeSpan); 
    }

}
