using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerReset : MonoBehaviour
{
    public GameObject camerafix;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player") && GameManager.instance.isVR == false)
        {
            camerafix.gameObject.SetActive(true);

        }
    }
}
