using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
//using Photon.Realtime;

public class LsSwitch : MonoBehaviour
{
    public LaserTurret Las;

    //public RawImage tl;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter(Collider other)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (other.CompareTag("Player"))
            {
                //other.gameObject.SetActive(false);
                //photonView.RPC("RpcRColor", RpcTarget.All);
                //RpcRColor();

                LaserTurret lt = Las.GetComponentInChildren<LaserTurret>();

                //Las.GetComponentInChildren<Camera>().enabled = true;
                //GameObject.Find("Camera").transform.GetChild(2).gameObject.SetActive(true);
                lt.TankCt(other.GetComponent<PhotonView>().ViewID);
            }
        }
    }

    public void BColor()
    {
        //this.gameObject.SetActive(true);
        //tl.GetComponent<RawImage>().enabled = true;
        //tl.GetComponent<BoxCollider>().enabled = true;
    }

    ////[PunRPC]
    //public void RpcRColor()
    //{
    //    this.gameObject.GetComponent<Renderer>().material.color = Color.red;
    //    gameObject.GetComponent<BoxCollider>().enabled = false;
    //}

}
