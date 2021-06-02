using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRotate : MonoBehaviour
{

    public float rotSpeed = 200;

    float rotX = 0;
    float rotY = 0;


    public bool useVertical = false;
    public bool useHorizontal = false;

    
    void Start()
    {
        
    }

    
    void Update()
    {
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        if(useVertical == true)
        {
            rotX += -my * rotSpeed * Time.deltaTime;
        }

        if(useHorizontal == true)
        {
            rotY += mx * rotSpeed * Time.deltaTime;
        }


        rotX = Mathf.Clamp(rotX, -90, 90);

        transform.localEulerAngles = new Vector3(rotX, rotY, 0);
    }
}
