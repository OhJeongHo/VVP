using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OJH_Laser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            print("vr РћСп");
            GameManager.instance.laserClose = true;
            GameManager.instance.vrClose = true;
        }
    }
}
