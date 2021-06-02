using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyRotate : MonoBehaviour
{
    // 회전 속력
    public float rotSpeed = 200;
    // 회전값
    float rotX;
    float rotY;

    // 회전 가능 여부
    public bool useVertical = false;
    public bool useHorizontal = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isVR)
        {
            return;
        }
        // 마우스의 움직임값을 받아오자.
        float mx = Input.GetAxis("Mouse X"); // 좌우에 따라서 1과 -1
        float my = Input.GetAxis("Mouse Y");

        // 회전 방향을 결정
        // Vector3 dir = Vector3.up * mx; // => ( 0, mx, 0 )
        // Vector3 dir = new Vector3(-my, mx, 0);

        // 회전 각도 누적
        if (useVertical == true) rotX += -my * rotSpeed * Time.deltaTime; // 상하회전
        if (useHorizontal == true) rotY += mx * rotSpeed * Time.deltaTime; // 좌우회전


        // 상하로 움직일 때 뒤집히는 문제가 발생함. -> 제한을 걸어줘야함.
        // rotX = Mathf.Clamp(rotX, -90, 90); // rotX 최소 -90 최대 90으로 고정해서 잡아줌

        rotX = Mathf.Clamp(rotX, -10, 0);


        // 받아온 값으로 물체를 회전하고 싶다. 오른쪽이면 ++, 왼쪽이면 --
        //transform.eulerAngles += dir * rotSpeed * Time.deltaTime; // vector3.up 으로 해야 x축을 기준으로 회전함.
        transform.localEulerAngles = new Vector3(rotX, rotY, 0);
    }
}
