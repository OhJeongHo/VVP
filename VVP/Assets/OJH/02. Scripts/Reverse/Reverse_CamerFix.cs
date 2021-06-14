using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Reverse_CamerFix : MonoBehaviour
{
    public Camera UICam;
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player") && GameManager.instance.isVR == false)
        {
            other.gameObject.SetActive(false);
            UICam.gameObject.SetActive(true);

        }
    }

    public void OnClickReturn()
    {
        gameObject.SetActive(false);
        UICam.gameObject.SetActive(false);
        player.SetActive(true);
    }

    public void OnClickBattleStart()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("OJH_Reverse");
        }
    }
}
