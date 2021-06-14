using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OJH_Vrmax : MonoBehaviour
{
    public GameObject mode1, mode2, mode3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mode1.activeSelf)
        {
            gameObject.GetComponent<Text>().text = "/ 1Έν";
        }
        if (mode2.activeSelf)
        {
            gameObject.GetComponent<Text>().text = "/ 10Έν";
        }
        if (mode3.activeSelf)
        {
            gameObject.GetComponent<Text>().text = "/ 2Έν";
        }
    }
}
