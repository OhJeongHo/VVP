using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OJH_Fuel : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 18)
        {
            photonView.RPC("RpcDestroy", RpcTarget.All);
            other.gameObject.layer = 17;
        }
    }

    [PunRPC]
    void RpcDestroy()
    {
        Destroy(gameObject);
    }
}
