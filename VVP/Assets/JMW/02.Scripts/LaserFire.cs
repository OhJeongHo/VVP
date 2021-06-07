using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFire : MonoBehaviour
{
    LineRenderer lr;
    
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.startColor = new Color(0.222f, 0, 1, 0.4f);

    }

    
    void Update()
    {

        //// 1��Ű�� ������
        //if(Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        //    RaycastHit hit;
        //    if(Physics.Raycast(ray, out hit))
        //    {

        //    }
        //}

        //1.��������ġ, �����վչ��� �߻��ϴ� Ray ����
        Ray ray = new Ray(transform.position, transform.forward);
        //2. �ε�����
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            //3. �ε��� �������� line �׸���
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, hit.point);
        }
    }
}
