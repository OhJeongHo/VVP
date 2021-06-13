using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class LBalpanColor : MonoBehaviourPun
{

    public RawImage tl;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void RColor()
    {
        //this.gameObject.GetComponent<Renderer>().material.color = Color.red;
        //gameObject.GetComponent<BoxCollider>().enabled = false;

        tl.GetComponent<RawImage>().enabled = false;
        tl.GetComponent<BoxCollider>().enabled = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (other.CompareTag("Player"))
            {

                photonView.RPC("RColor", RpcTarget.All);

                
            }
        }
    }
}
