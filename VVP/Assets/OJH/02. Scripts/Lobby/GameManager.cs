using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using Photon.Pun;


public class GameManager : MonoBehaviourPun
{
    public static GameManager instance;
    

    // public LobbyActive myplayer;

    public bool isVR;
    public int rocketCnt;

    public static bool isPresent()
    {
        var xrDisplaySubsystems = new List<XRDisplaySubsystem>();
        SubsystemManager.GetInstances<XRDisplaySubsystem>(xrDisplaySubsystems);
        foreach (var xrDisplay in xrDisplaySubsystems)
        {
            if (xrDisplay.running)
            {
                return true;
            }
        }
        return false;
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;

        Debug.Log("VR Device = " + isPresent().ToString());
        isVR = isPresent();
    }

    private void Start()
    {
        // Rigidbody rb;
        // rb.AddExplosionForce()
        // rb.AddForceAtPosition()
    }

    private void Update()
    {


    }


    public void RocketCount(int addValue)
    {
        rocketCnt += addValue;
    }

    
    //public void OtherRocketImg(int addvalue)
    //{
    //    otherRocketCnt += addvalue;
    //    if (otherRocketCnt == 0)
    //    {
    //        if (otherRocket.GetComponent<MeshRenderer>().enabled == true)
    //        {
    //            otherRocket.GetComponent<MeshRenderer>().enabled = false;
    //        }
    //    }
    //    if (otherRocketCnt > 0)
    //    {
    //        if (otherRocket.GetComponent<MeshRenderer>().enabled == false)
    //        {
    //            otherRocket.GetComponent<MeshRenderer>().enabled = true;
    //        }
    //    }
    //}
}

