using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OJH_Rocket : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Destroy(gameObject);
            GameManager.instance.RocketImg(1);
        }
    }
}
