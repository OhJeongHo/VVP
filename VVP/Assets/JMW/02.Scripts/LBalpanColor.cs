using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LBalpanColor : MonoBehaviourPun
{
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
        this.gameObject.GetComponent<Renderer>().material.color = Color.red;
        gameObject.GetComponent<BoxCollider>().enabled = false;
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
