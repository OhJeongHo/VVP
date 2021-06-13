using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public float gameTime = 210;
    float currTime;

    public Text GameTimeText;

    //시간담당
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime;

        if (gameTime - currTime <= 0)
        {
            // 게임 종료 VR의 승리

        }




        //if ((int)gameTime == 0)
        //{
        //}
        //else
        //{
        //    gameTime -= Time.deltaTime;
        //    GameTimeText.text = "Time : " + (int)(gameTime / 60) % 60 + ":" + (int)(gameTime % 60);

        //    return;
        //}
    }
}
