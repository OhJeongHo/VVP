using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OJH_GrabObj : MonoBehaviour
{
    private Animator _anim;

    LineRenderer lr;

    // 잡은 놈의 트랜스폼
    Transform catchedObj;

    // 던지는 힘
    public float throwPower = 10f;

    // 큐브 공장
    public GameObject cubeFactory;
    // 왼손 트랜스폼
    public Transform leftHand;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        _anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // DrawGuideLine();
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

        int layerMask = 1 << 6;
        int layer = 1 << 7;
        int layer2 = 1 << 8;
        int layer3 = 1 << 12;


        layerMask = ~(layerMask | layer2 | layer | layer3);

        if(OVRInput.GetUp(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            
            _anim.SetBool("IsGrabbing", false);
        }

        if(OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        { 
            Collider [] hits = Physics.OverlapSphere(transform.position, 0.7f, layerMask);

            _anim.SetBool("IsGrabbing", true);
            //if (!_anim.GetBool("IsGrabbing"))
            //{
               
            //}
            //else
            //{                
            //    if (_anim.GetBool("IsGrabbing"))
            //    {
            //        _anim.SetBool("IsGrabbing", false);
            //    }
            //}

            //Ray ray = new Ray(transform.position, transform.forward);
            //// 반지름이 r인 구모양을 발사한다
            //RaycastHit hit;
            //if(Physics.SphereCast(ray, 0.5f, out hit, 100))

            if (hits.Length > 0) // 위의 주석처리를 사용하려면 hit로 사용하면 되고, 이걸 쓰려면 hits[0]으로
            {
                // 잡은 놈 저장
                catchedObj = hits[0].transform;

                if (catchedObj.GetComponent<OJH_BattlePlayer>() != null)
                {
                    catchedObj.GetComponent<CharacterController>().enabled = false;
                    if (catchedObj.GetComponent<OJH_BattlePlayer>().sternMode == false)
                    {
                        catchedObj.GetComponent<OJH_BattlePlayer>().sternMode = true;
                    }
                }
                // 부딪힌 놈을 오른속 자식으로 놓는다
                hits[0].transform.SetParent(transform);
                // 부딪힌 놈 물리연산이 되지 않도록
                Rigidbody rb = hits[0].transform.GetComponent<Rigidbody>();
                rb.isKinematic = true;

                //// 잡은 놈을 손 위치로
                //// hit.transform.position = transform.position
                //hits[0].transform.localPosition = Vector3.zero;

                //// 큐브 공장에서 큐브 만든다
                //GameObject cube = Instantiate(cubeFactory);

                //// 만든 큐브를 왼쪽 손에 붙인다
                //cube.transform.SetParent(leftHand);

                //// 만든 큐브를 왼손 좌표에 위치시킨다
                //cube.transform.localPosition = Vector3.zero;
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
        if (OVRInput.GetUp(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {

            if (_anim.GetBool("IsGrabbing"))
            {
                _anim.SetBool("IsGrabbing", false);
            }
            // 잡고 있는 오브젝트의 부모를 null로
            catchedObj.SetParent(null);
            // 잡고있는 오브젝트 -> 리기드바디 -> 이즈키네메틱을 false로
            catchedObj.GetComponent<Rigidbody>().isKinematic = false;

            if (catchedObj.GetComponent<OJH_BattlePlayer>() != null)
            {
                catchedObj.GetComponent<CharacterController>().enabled = true;
                catchedObj.GetComponent<OJH_BattlePlayer>().SternReset2();
            }

            // 던지자
            ThrowObj(transform.TransformDirection(OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch) * 3),
                transform.TransformDirection(OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.RTouch)));

            // 잡은 놈 저장한 것 리셋
            catchedObj = null;

        }
    }

    void ThrowObj(Vector3 velocity, Vector3 angleVelocity)
    {
        // 컨트롤러 속도(방향, 크기)로 던진다
        // 리지드바디 가져온다
        Rigidbody rb = catchedObj.GetComponent<Rigidbody>();
        // 속도 셋팅
        rb.velocity = velocity;
        // 각속도 셋팅
        rb.angularVelocity = angleVelocity;
    }

    //void ThrowObj()
    //{
    //    // 컨트롤러 속도(방향, 크기)로 던진다
    //    // 리지드바디 가져온다
    //    Rigidbody rb = catchedObj.GetComponent<Rigidbody>();
    //    // 속도 셋팅
    //    rb.velocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch) * throwPower;
    //    // 각속도 셋팅
    //    rb.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.RTouch);
    //}

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
