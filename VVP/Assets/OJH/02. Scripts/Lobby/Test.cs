using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Test : MonoBehaviourPun
{
    public GameObject my;
    public GameObject other;
    // Start is called before the first frame update
    void Start()
    {
        //if(photonView.IsMine)
        //{
        //    my.SetActive(true);
        //}
        //else
        //{
        //    other.SetActive(true);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                transform.position += transform.right;
            }
        }
    }
}
