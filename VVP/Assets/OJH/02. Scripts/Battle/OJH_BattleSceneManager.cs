using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class OJH_BattleSceneManager : MonoBehaviourPunCallbacks
{
    public Transform vrpoint;
    public Transform pcpoint;
    
    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
