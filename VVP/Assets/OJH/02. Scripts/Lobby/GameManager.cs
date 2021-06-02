using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isVR;

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
    public void OnPcClick()
    {
        SceneManager.LoadScene("OJH_LobbyScene");
    }

    public void OnVrClick()
    {
        SceneManager.LoadScene("OJH_LobbyScene");
    }
}
