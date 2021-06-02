using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyRotate : MonoBehaviour
{
    // ȸ�� �ӷ�
    public float rotSpeed = 200;
    // ȸ����
    float rotX;
    float rotY;

    // ȸ�� ���� ����
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
        // ���콺�� �����Ӱ��� �޾ƿ���.
        float mx = Input.GetAxis("Mouse X"); // �¿쿡 ���� 1�� -1
        float my = Input.GetAxis("Mouse Y");

        // ȸ�� ������ ����
        // Vector3 dir = Vector3.up * mx; // => ( 0, mx, 0 )
        // Vector3 dir = new Vector3(-my, mx, 0);

        // ȸ�� ���� ����
        if (useVertical == true) rotX += -my * rotSpeed * Time.deltaTime; // ����ȸ��
        if (useHorizontal == true) rotY += mx * rotSpeed * Time.deltaTime; // �¿�ȸ��


        // ���Ϸ� ������ �� �������� ������ �߻���. -> ������ �ɾ������.
        // rotX = Mathf.Clamp(rotX, -90, 90); // rotX �ּ� -90 �ִ� 90���� �����ؼ� �����

        rotX = Mathf.Clamp(rotX, -10, 0);


        // �޾ƿ� ������ ��ü�� ȸ���ϰ� �ʹ�. �������̸� ++, �����̸� --
        //transform.eulerAngles += dir * rotSpeed * Time.deltaTime; // vector3.up ���� �ؾ� x���� �������� ȸ����.
        transform.localEulerAngles = new Vector3(rotX, rotY, 0);
    }
}
