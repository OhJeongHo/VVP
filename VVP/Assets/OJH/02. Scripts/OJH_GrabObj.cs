using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OJH_GrabObj : MonoBehaviour
{
    LineRenderer lr;

    // ���� ���� Ʈ������
    Transform catchedObj;

    // ������ ��
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
            // �ε��� ���� ������ �����տ��� ����� �� ������ ���� �׷���
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, transform.position + transform.forward * 1);
        }
    }
    void CatchObj()
    {
        if(OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        { 
            Ray ray = new Ray(transform.position, transform.forward);
            // �������� r�� ������� �߻��Ѵ�
            RaycastHit hit;
            if(Physics.SphereCast(ray, 0.5f, out hit, 100))
            {
                // ���� �� ����
                catchedObj = hit.transform;
                // �ε��� ���� ������ �ڽ����� ���´�
                hit.transform.SetParent(transform);
                // �ε��� �� ���������� ���� �ʵ���
                Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                rb.isKinematic = true;

                // ���� ���� �� ��ġ��
                // hit.transform.position = transform.position
                hit.transform.localPosition = Vector3.zero;
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
            // ��� �ִ� ������Ʈ�� �θ� null��
            catchedObj.SetParent(null);
            // ����ִ� ������Ʈ -> �����ٵ� -> ����Ű�׸�ƽ�� false��
            catchedObj.GetComponent<Rigidbody>().isKinematic = false;

            // ������
            ThrowObj();

            // ���� �� ������ �� ����
            catchedObj = null;

        }
    }

    void ThrowObj()
    {
        // ��Ʈ�ѷ� �ӵ�(����, ũ��)�� ������
        // ������ٵ� �����´�
        Rigidbody rb = catchedObj.GetComponent<Rigidbody>();
        // �ӵ� ����
        rb.velocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
    }
}
