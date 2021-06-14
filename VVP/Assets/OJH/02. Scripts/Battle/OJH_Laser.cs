using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OJH_Laser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            GameManager.instance.laserClose = true;
            GameManager.instance.vrClose = true;
        }
        GameObject BigExplo = PhotonNetwork.Instantiate("BigExplosion", transform.position, Quaternion.identity);
        

    }

   
}
