using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OJH_Rocket : MonoBehaviour
{
    public GameObject playerRocket;

    
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
        if(other.gameObject.layer == 7)
        {
            GameManager.instance.rocketCnt++;
            Destroy(gameObject, 0.01f);
        }
    }
}
