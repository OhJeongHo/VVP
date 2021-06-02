using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kinematic : MonoBehaviour
{

    Rigidbody rb;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }


    //public void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.tag == "Object")
    //    {
    //        collision.rigidbody.isKinematic = false;
            
            
    //    }
    //}

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Object"))
        {
            rb.isKinematic = false;

           
        }
    }

}
