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
        if ((int)GameTime == 0)
        {
        }
        else
        {
            GameTime -= Time.deltaTime;
            GameTimeText.text = "Time : " + (int)(GameTime / 60) % 60 + ":" + (int)(GameTime % 60);

            return;
        }

    }


    public void RocketImg(int addValue)
    {
        rocketCnt += addValue;

        if (rocketCnt == 0)
        {
            if (playerRocket.GetComponent<MeshRenderer>().enabled == true)
            {
                playerRocket.GetComponent<MeshRenderer>().enabled = false; 
            }
            //myplayer.등짝붙은애 보여주는 함수호출
        }
        if (rocketCnt > 0)
        {
            if (playerRocket.GetComponent<MeshRenderer>().enabled == false)
            {
                playerRocket.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }
}
