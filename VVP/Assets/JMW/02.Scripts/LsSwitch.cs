using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LsSwitch : MonoBehaviour
{

    

    public LaserTurret Las;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("플레이어충돌");
            other.gameObject.SetActive(false);
            RColor();
            Las.GetComponentInChildren<LaserTurret>().enabled = true;
            Las.GetComponentInChildren<LaserTurret>().pcplayer = other.gameObject;
            Las.GetComponentInChildren<Camera>().enabled = true;
            GameObject.Find("Camera").transform.GetChild(2).gameObject.SetActive(true);
            Las.GetComponentInChildren<LaserTurret>().TankCt();


        }
    }

    public void BColor()
    {
        this.gameObject.GetComponent<Renderer>().material.color = Color.blue;
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    public void RColor()
    {
        this.gameObject.GetComponent<Renderer>().material.color = Color.red;
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

}
