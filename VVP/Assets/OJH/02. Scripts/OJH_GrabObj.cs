using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OJH_GrabObj : MonoBehaviour
{
    private Animator _anim;

    LineRenderer lr;

    // ���� ���� Ʈ������
    Transform catchedObj;

    // ������ ��
    public float throwPower = 2f;

    // ť�� ����
    public GameObject cubeFactory;
    // �޼� Ʈ������
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
            // �ε��� ���� ������ �����տ��� ����� �� ������ ���� �׷���
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, transform.position + transform.forward * 1);
        }
    }
    void CatchObj()
    {

        int layerMask = 1 << 6;

        layerMask = ~layerMask;

        if(OVRInput.GetUp(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
            
            _anim.SetBool("IsGrabbing", false);
        }

        if(OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        { 
            Collider [] hits = Physics.OverlapSphere(transform.position, 0.3f, layerMask);

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
            //// �������� r�� ������� �߻��Ѵ�
            //RaycastHit hit;
            //if(Physics.SphereCast(ray, 0.5f, out hit, 100))

            if (hits.Length > 0) // ���� �ּ�ó���� ����Ϸ��� hit�� ����ϸ� �ǰ�, �̰� ������ hits[0]����
            {
                // ���� �� ����
                catchedObj = hits[0].transform;
                // �ε��� ���� ������ �ڽ����� ���´�
                hits[0].transform.SetParent(transform);
                // �ε��� �� ���������� ���� �ʵ���
                Rigidbody rb = hits[0].transform.GetComponent<Rigidbody>();
                rb.isKinematic = true;

                // ���� ���� �� ��ġ��
                // hit.transform.position = transform.position
                hits[0].transform.localPosition = Vector3.zero;

                // ť�� ���忡�� ť�� �����
                GameObject cube = Instantiate(cubeFactory);

                // ���� ť�긦 ���� �տ� ���δ�
                cube.transform.SetParent(leftHand);

                // ���� ť�긦 �޼� ��ǥ�� ��ġ��Ų��
                cube.transform.localPosition = Vector3.zero;
            }
        }
    }

    void DropObj()
    {
        // ���࿡ ���� ���� ���ٸ� �Լ��� ������
        if (catchedObj == null)
        {
            return;
        }
        // ������ B ��ư�� ����
        if (OVRInput.GetUp(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {

            if (_anim.GetBool("IsGrabbing"))
            {
                _anim.SetBool("IsGrabbing", false);
            }
            // ��� �ִ� ������Ʈ�� �θ� null��
            catchedObj.SetParent(null);
            // ����ִ� ������Ʈ -> �����ٵ� -> ����Ű�׸�ƽ�� false��
            catchedObj.GetComponent<Rigidbody>().isKinematic = false;

            // ������
            ThrowObj(transform.TransformDirection(OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch) * 3),
                transform.TransformDirection(OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.RTouch)));

            // ���� �� ������ �� ����
            catchedObj = null;

        }
    }

    void ThrowObj(Vector3 velocity, Vector3 angleVelocity)
    {
        // ��Ʈ�ѷ� �ӵ�(����, ũ��)�� ������
        // ������ٵ� �����´�
        Rigidbody rb = catchedObj.GetComponent<Rigidbody>();
        // �ӵ� ����
        rb.velocity = velocity;
        // ���ӵ� ����
        rb.angularVelocity = angleVelocity;
    }

    //void ThrowObj()
    //{
    //    // ��Ʈ�ѷ� �ӵ�(����, ũ��)�� ������
    //    // ������ٵ� �����´�
    //    Rigidbody rb = catchedObj.GetComponent<Rigidbody>();
    //    // �ӵ� ����
    //    rb.velocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch) * throwPower;
    //    // ���ӵ� ����
    //    rb.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.RTouch);
    //}
}