using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{


    public Camera Camera1;
    public Camera Camera2;
    public Camera Camera3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.Alpha1))
        {
            Camera1.gameObject.SetActive(true);
            Camera2.gameObject.SetActive(false);
            Camera3.gameObject.SetActive(false);  
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            Camera1.gameObject.SetActive(false);
            Camera2.gameObject.SetActive(true);
            Camera3.gameObject.SetActive(false);  
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            Camera1.gameObject.SetActive(false);
            Camera2.gameObject.SetActive(false);
            Camera3.gameObject.SetActive(true);  
        }

    }
}
