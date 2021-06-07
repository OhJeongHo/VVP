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

        //// 1번키를 누르면
        //if(Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        //    RaycastHit hit;
        //    if(Physics.Raycast(ray, out hit))
        //    {

        //    }
        //}

        //1.오른손위치, 오른손앞방향 발사하는 Ray 만듬
        Ray ray = new Ray(transform.position, transform.forward);
        //2. 부딪히곳
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            //3. 부딪힌 지점까지 line 그린다
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, hit.point);
        }
    }
}
