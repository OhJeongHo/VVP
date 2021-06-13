using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public RawImage textTitle;
    public RawImage textTitle2;

    //알파값
    float a = 0;
    float dir = 1;

    void Start()
    {



    }


    void Update()
    {
        //알파값이 점점 늘어나게한후
        a += 0.01f * dir;
        //컬러값에 세팅해준다
        textTitle.color = new Color(1, 1, 1, a);
        textTitle2.color = new Color(1, 1, 1, a);
        //3. 만약에 a가 1보다 같거나 커지면 a는 0으로
        if (a >= 1) dir *= -1;
        if (a <= 0) dir *= -1;

    }
    
}