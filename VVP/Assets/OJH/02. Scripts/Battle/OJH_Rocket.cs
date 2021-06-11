using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OJH_Rocket : MonoBehaviourPun
{
    float currTime;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime;

        if (currTime > 10)
        {
            Destroy(gameObject);
            currTime = 0;
        }
    }



}
