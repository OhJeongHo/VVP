using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OJH_RHand : MonoBehaviour
{
    public GameObject handModel;
    public GameObject leftHand;
    public GameObject rock;
    Quaternion rot;
    Vector3 pos;

    bool clap;
    float currTime;
    float setTime = 10;
    // Start is called before the first frame update
    void Start()
    {
        rot = handModel.transform.localRotation;
        pos = handModel.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (clap == false)
        {
            currTime += Time.deltaTime;
            if (currTime >= setTime)
            {
                clap = true;
                currTime = 0;
            }
        }
    }
    void Rock()
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            GameObject rocks = PhotonNetwork.Instantiate("Rock", transform.position, Quaternion.identity);
            //rocks.transform.position = transform.position;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            handModel.transform.parent = null;
            Rock();
        }
        if (other.gameObject == leftHand)
        {
            print("¹Ú¼ö");
            if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
            {
                clap = false;
                GameObject Clap = PhotonNetwork.Instantiate("ClapBoom", transform.position, Quaternion.identity);
            }
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
