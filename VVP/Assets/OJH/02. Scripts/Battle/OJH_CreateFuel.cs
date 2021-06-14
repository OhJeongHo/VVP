using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OJH_CreateFuel : MonoBehaviourPun
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

        if (currTime > 9)
        {
            float newX = Random.Range(-27f, 32f), newZ = Random.Range(-2.4f, 37f);
            PhotonNetwork.Instantiate("Fuel", new Vector3(newX, gameObject.transform.position.y, newZ), Quaternion.Euler(-90, 0, 0));
            // 포톤으로 연료 생성
            currTime = 0;
        }

        if (currTime2 > 9.5f)
        {
            float newX = Random.Range(-27f, 32f), newZ = Random.Range(-2.4f, 37f);
            PhotonNetwork.Instantiate("Fuel", new Vector3(newX, gameObject.transform.position.y, newZ), Quaternion.Euler(-90, 0, 0));
            currTime2 = 0;
        }

        if (currTime3 > 10)
        {
            float newX = Random.Range(-27f, 32f), newZ = Random.Range(-2.4f, 37f);
            PhotonNetwork.Instantiate("Fuel", new Vector3(newX, gameObject.transform.position.y, newZ), Quaternion.Euler(-90, 0, 0));
            // 포톤으로 연료 생성
            currTime3 = 0;
        }
    }
}
