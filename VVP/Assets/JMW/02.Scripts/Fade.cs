using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public RawImage textTitle;
    public RawImage textTitle2;

    //���İ�
    float a = 0;
    float dir = 1;

    void Start()
    {



    }


    void Update()
    {
        //���İ��� ���� �þ������
        a += 0.01f * dir;
        //�÷����� �������ش�
        textTitle.color = new Color(1, 1, 1, a);
        textTitle2.color = new Color(1, 1, 1, a);
        //3. ���࿡ a�� 1���� ���ų� Ŀ���� a�� 0����
        if (a >= 1) dir *= -1;
        if (a <= 0) dir *= -1;

    }
    
}