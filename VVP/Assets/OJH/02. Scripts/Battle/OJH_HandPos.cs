using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OJH_HandPos : MonoBehaviour
{
    public GameObject handModel;
    Quaternion originRot;
    Quaternion rot;
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        // rot = handModel.transform.rotation;
        rot = handModel.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            pos = handModel.transform.position;
            originRot = handModel.transform.rotation;
            handModel.transform.parent = null;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            // handModel.transform.position = pos;
            // handModel.transform.rotation = originRot;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            handModel.transform.parent = transform;
            handModel.transform.position = transform.position;
            handModel.transform.localRotation = rot;
        }
    }
}
