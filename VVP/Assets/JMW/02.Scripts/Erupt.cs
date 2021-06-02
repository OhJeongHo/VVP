using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Erupt : MonoBehaviour
{
    public float tPower = 715;
    public GameObject eruptFactory;
    public Transform firePos;

    void Start()
    {
        
    }

    
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
        GameObject erup = Instantiate(eruptFactory);
        erup.transform.position = firePos.position;
        Rigidbody rb = erup.GetComponent<Rigidbody>();
        rb.AddForce(Camera.main.transform.up * tPower);
        Destroy(erup,1);

        }
    }
}
