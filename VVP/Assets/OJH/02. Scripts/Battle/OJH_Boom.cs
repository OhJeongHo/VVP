using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OJH_Boom : MonoBehaviour
{
    // public Transform explo;
    // public Rigidbody[] rbs;
    // Start is called before the first frame update
    Collider[] colls;
    void Start()
    {
        // rb.AddExplosionForce(10f, transform.position, 0.5f);
        colls = Physics.OverlapSphere(transform.position, 1f);

        
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            foreach (Collider coll in colls)
            {
                if (coll.GetComponent<CharacterController>() != null)
                {
                    print("cc Á¸Àç");
                    coll.GetComponent<CharacterController>().enabled = false;
                    coll.GetComponent<Rigidbody>().isKinematic = false;
                }
                if (coll.attachedRigidbody != null)
                {
                    coll.attachedRigidbody.AddExplosionForce(100f, transform.position, 2f, 1f);
                }
                
            }
        }
    }
}
