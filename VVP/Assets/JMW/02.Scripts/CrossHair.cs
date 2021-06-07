using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //1, Ray ����� (ī�޶���ġ, ī�޶� �չ���)
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        //2. �ε��� ������ crosshair ��ġ��Ų��
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            //��ġ��Ų��
            transform.position = hit.point;
            //ũ������
            float dist = Vector3.Distance(
                Camera.main.transform.position, hit.point);
            transform.localScale = Vector3.one * dist;
        }
    }
}
