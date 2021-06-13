using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PCUI : MonoBehaviour
{
    public Text RocketCnt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RocketCnt.text = "X " + GameManager.instance.rocketCnt;
    }
}
