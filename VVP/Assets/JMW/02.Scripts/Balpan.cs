using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balpan : MonoBehaviour
{
    Renderer AColor;

    public Tower owner;

    public void ChangeSwitch()
    {
        AColor = gameObject.GetComponent<Renderer>();
    }
    
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {


            this.gameObject.GetComponent<Renderer>().material.color = Color.red;

            GameObject.Find("HMC").GetComponent<Tower>().shoots();

            gameObject.GetComponent<BoxCollider>().enabled = false;

            Invoke("OnSwitchon", 10);


        }

    }

    public void OnSwitchon()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;

        this.gameObject.GetComponent<Renderer>().material.color = Color.blue;
    }

    


}
