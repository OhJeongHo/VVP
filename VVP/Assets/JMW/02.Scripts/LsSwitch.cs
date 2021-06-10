using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LsSwitch : MonoBehaviour
{
    public LaserTurret Las;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if(PhotonNetwork.IsMasterClient)
        {
            if (other.CompareTag("Player"))
            {
                print("플레이어충돌");
                //other.gameObject.SetActive(false);
                RColor();
                LaserTurret lt = Las.GetComponentInChildren<LaserTurret>();
                
                //Las.GetComponentInChildren<Camera>().enabled = true;
                //GameObject.Find("Camera").transform.GetChild(2).gameObject.SetActive(true);
                lt.TankCt(other.GetComponent<PhotonView>().ViewID);
            }
        }
    }

    public void BColor()
    {
        this.gameObject.GetComponent<Renderer>().material.color = Color.blue;
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    public void RColor()
    {
        this.gameObject.GetComponent<Renderer>().material.color = Color.red;
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

}
