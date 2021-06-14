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
    public int fuelCnt = 1;
    public bool vrwin = false;
    public bool vrlose = false;
    public bool vrClose = false;
    public bool laserClose = false;
    public bool rocketClose = false;

    public PhotonView myPhotonView;
    public List<PhotonView> players;

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

    public void FuelCount(int addValue)
    {
        fuelCnt += addValue;
    }
    public PhotonView GetPhotonView(int viewId)
    {
        for(int i = 0; i < players.Count; i++)
        {
            if (players[i].ViewID == viewId)
                return players[i];
        }
        return null;
    }
}

