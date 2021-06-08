using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OJH_LHand : MonoBehaviour
{
    public GameObject handModel;
    public GameObject rock;
    Quaternion rot;
    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        rot = handModel.transform.localRotation;
        pos = handModel.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Rock()
    {
        if (OVRInput.Get(OVRInput.Button.Any, OVRInput.Controller.LTouch))
        {
            GameObject rocks = PhotonNetwork.Instantiate("Rock", transform.position, Quaternion.identity);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            handModel.transform.parent = null;
            Rock();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 6)
        {

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            handModel.transform.parent = transform;
            handModel.transform.localPosition = pos;
            handModel.transform.localRotation = rot;
        }
    }
}