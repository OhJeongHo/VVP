using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OJH_GrabObj : MonoBehaviour
{
    LineRenderer lr;

    // 잡은 놈의 트랜스폼
    Transform catchedObj;

    // 던지는 힘
    public float throwPower;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        DrawGuideLine();
        CatchObj();
        DropObj();
    }


    void DrawGuideLine()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, hit.point); ;
        }
        else
        {
            // 부딪힌 지점 없으면 오른손에서 몇미터 앞 까지만 라인 그려라
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, transform.position + transform.forward * 1);
        }
    }
    void CatchObj()
    {
        if(OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        { 
            Ray ray = new Ray(transform.position, transform.forward);
            // 반지름이 r인 구모양을 발사한다
            RaycastHit hit;
            if(Physics.SphereCast(ray, 0.5f, out hit, 100))
            {
                // 잡은 놈 저장
                catchedObj = hit.transform;
                // 부딪힌 놈을 오른속 자식으로 놓는다
                hit.transform.SetParent(transform);
                // 부딪힌 놈 물리연산이 되지 않도록
                Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                rb.isKinematic = true;

                // 잡은 놈을 손 위치로
                // hit.transform.position = transform.position
                hit.transform.localPosition = Vector3.zero;
            }
        }
    }

    void DropObj()
    {
        // 만약에 잡은 놈이 없다면 함수를 나가라
        if (catchedObj == null)
        {
            return;
        }
        // 오른손 B 버튼을 떼면
        if (OVRInput.GetUp(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
            // 잡고 있는 오브젝트의 부모를 null로
            catchedObj.SetParent(null);
            // 잡고있는 오브젝트 -> 리기드바디 -> 이즈키네메틱을 false로
            catchedObj.GetComponent<Rigidbody>().isKinematic = false;

            // 던지자
            ThrowObj();

            // 잡은 놈 저장한 것 리셋
            catchedObj = null;

        }
    }

    void ThrowObj()
    {
        // 컨트롤러 속도(방향, 크기)로 던진다
        // 리지드바디 가져온다
        Rigidbody rb = catchedObj.GetComponent<Rigidbody>();
        // 속도 셋팅
        rb.velocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
    }
}
