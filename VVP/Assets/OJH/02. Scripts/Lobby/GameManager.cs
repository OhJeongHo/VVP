using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float GameTime = 180;
    public Text GameTimeText;
    //시간담당


    public static GameManager instance;
    public GameObject playerRocket;

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
        RocketImg();

        if ((int)GameTime == 0)
        {
        }
        else
        {
            GameTime -= Time.deltaTime;
            GameTimeText.text = "Time : " + (int)(GameTime/60)%60 + ":" + (int)(GameTime%60);

            return;
        }
    }

    void RocketImg()
    {
        if (rocketCnt == 0)
        {
            playerRocket.SetActive(false);
        }
        if (rocketCnt > 0)
        {
            playerRocket.SetActive(true);
        }
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
