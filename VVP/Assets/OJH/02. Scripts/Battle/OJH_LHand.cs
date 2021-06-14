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
    float laserTime;
    bool handout = false;
    // Start is called before the first frame update
    void Start()
    {
        rot = handModel.transform.localRotation;
        pos = handModel.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.laserClose)
        {
            handModel.transform.parent = null;
            laserTime += Time.deltaTime;

            if (laserTime > 5)
            {
                handout = true;
                laserTime = 0;
            }
        }
        if (handout)
        {
            handModel.transform.parent = transform;
            handModel.transform.localPosition = pos;
            handModel.transform.localRotation = rot;
            handout = false;
        }
        if (GameManager.instance.vrClose)
        {
            return;
        }
    }
    void Rock()
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            GameObject rocks = PhotonNetwork.Instantiate("Rock", transform.position, Quaternion.identity);
            GameObject rParti = PhotonNetwork.Instantiate("DustSmoke", transform.position, Quaternion.identity);
            StartCoroutine(DestroyAfter(rParti, 1f));

        }
    }

    IEnumerator DestroyAfter(GameObject target, float delay)
    {
        yield return new WaitForSeconds(delay);
        PhotonNetwork.Destroy(target);
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