using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OJH_Lobby_VrPlayerMove : MonoBehaviour
{
    PhotonView photonview;

    public float speed = 2;
    void Start()
    {
        photonview = GetComponentInParent<PhotonView>();
      
    }

    void Update()
    {
        if (photonview.IsMine == false)
        {
            return;
        }
        ////���� ���̽�ƽ �� �޾ƿ���
        //Vector2 joystickL = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);

        ////���� ������
        //Vector3 dir = transform.forward * joystickL.y +
        //    transform.transform.right * joystickL.x;
        //dir.Normalize();

        ////�׹������� �̵�����
        //transform.position += dir * speed * Time.deltaTime;

        //���� ���̽�ƽ �� �޾ƿ���
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            transform.position += transform.forward * 1;
        }

        Vector2 joyStickR = OVRInput.Get(
            OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);

        transform.Rotate(0, joyStickR.x * 70 * Time.deltaTime, 0);
    }
}
