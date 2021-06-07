using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public float GameTime = 180;
    public Text GameTimeText;
    //시간담당
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((int)GameTime == 0)
        {
        }
        else
        {
            GameTime -= Time.deltaTime;
            GameTimeText.text = "Time : " + (int)(GameTime / 60) % 60 + ":" + (int)(GameTime % 60);

            return;
        }
    }
}
