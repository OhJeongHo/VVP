using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LaserCol : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        GameObject Laserexplo = PhotonNetwork.Instantiate("BigExplosion", transform.position, Quaternion.identity);
        print("ºÎµúÈû");
    }
}
