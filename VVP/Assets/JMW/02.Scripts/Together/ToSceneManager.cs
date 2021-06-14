using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ToSceneManager : MonoBehaviour
{
    public Transform vrpoint;
    public Transform pcpoint;
   
    void Start()
    {
        if (GameManager.instance.isVR)
        {
            PhotonNetwork.Instantiate("BattlePlayer", vrpoint.position, Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate("BattlePlayer", pcpoint.position, Quaternion.Euler(0, 180, 0));
        }
    }


    void Update()
    {
        
    }
}
