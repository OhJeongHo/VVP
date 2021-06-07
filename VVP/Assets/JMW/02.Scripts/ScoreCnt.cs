using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCnt : MonoBehaviour
{
    Text text;
    public static int vrCnt = 0;
    void Start()
    {
        text = GetComponent<Text>();
    }

    
    void Update()
    {
        text.text = "VR " + vrCnt.ToString() + "-" + "0 PC";
    }
}
