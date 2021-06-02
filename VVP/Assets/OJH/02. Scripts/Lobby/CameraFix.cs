using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFix : MonoBehaviour
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
}
