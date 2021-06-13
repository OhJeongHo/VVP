using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OJH_CreateRocket : MonoBehaviourPun
{
    float currTime, currTime2, currTime3;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime;
        currTime2 += Time.deltaTime;
        currTime3 += Time.deltaTime;

        if (currTime > 20)
        {
            float newX = Random.Range(-27f, 32f), newZ = Random.Range(-2.4f, 37f);
            PhotonNetwork.Instantiate("Rocket", new Vector3(newX, gameObject.transform.position.y, newZ), Quaternion.Euler(0, 0, 0));
            // transform.position = new Vector3(newX, gameObject.transform.position.y, newZ);
            currTime = 0;
        }
        if (currTime2 > 19)
        {
            float newX = Random.Range(-27f, 32f), newZ = Random.Range(-2.4f, 37f);
            PhotonNetwork.Instantiate("Rocket", new Vector3(newX, gameObject.transform.position.y, newZ), Quaternion.Euler(0, 0, 0));
            // transform.position = new Vector3(newX, gameObject.transform.position.y, newZ);
            currTime2 = 0;
        }
        if (currTime3 > 18)
        {
            float newX = Random.Range(-27f, 32f), newZ = Random.Range(-2.4f, 37f);
            PhotonNetwork.Instantiate("Rocket", new Vector3(newX, gameObject.transform.position.y, newZ), Quaternion.Euler(0, 0, 0));
            // transform.position = new Vector3(newX, gameObject.transform.position.y, newZ);
            currTime3 = 0;
        }
    }
}