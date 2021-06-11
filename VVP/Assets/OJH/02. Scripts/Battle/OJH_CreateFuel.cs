using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OJH_CreateFuel : MonoBehaviourPun
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
            print("로켓 생성");
            float newX = Random.Range(-27f, 32f), newZ = Random.Range(-2.4f, 37f);
            PhotonNetwork.Instantiate("Fuel", new Vector3(newX, gameObject.transform.position.y, newZ), Quaternion.Euler(0, 0, 0));
            // transform.position = new Vector3(newX, gameObject.transform.position.y, newZ);
            float newX2 = Random.Range(-27f, 32f), newZ2 = Random.Range(-2.4f, 37f);
            PhotonNetwork.Instantiate("Fuel", new Vector3(newX2, gameObject.transform.position.y, newZ2), Quaternion.Euler(0, 0, 0));
            // 포톤으로 연료 생성
            currTime = 0;
        }
    }
}
