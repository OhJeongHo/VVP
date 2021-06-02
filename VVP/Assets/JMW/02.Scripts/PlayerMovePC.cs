using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovePC : MonoBehaviour
{
  

    enum PlayerState
    {
        Idle,
        Run,
        Jump,
        Fly,
        Stern
    }


    //���¸���������
    PlayerState state;

    // ĳ���� ��Ʈ��
    CharacterController cc;
    // �ִϸ�����
    Animator anim;
    // �߷�
    //float gravity = -9.8f;
    // ���� �Ŀ�
    float jumpPower = 3;
    // y �ӷ�
    float yVelocity;
    // ���� Ƚ��
    int jumpCnt = 0;
    // �ִ� ���� Ƚ��
    int maxjumpcnt = 1;

    // �̵� ����
    Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        state = PlayerState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case PlayerState.Idle:
                Idle();
                break;
            case PlayerState.Run:
                Run();
                break;
            case PlayerState.Jump:
                Jump();
                break;
            case PlayerState.Fly:
                Fly();
                break;
            case PlayerState.Stern:
                Stern();
                break;
            default:
                break;
        }

        PcPlayerMove();



    }

    void PcPlayerMove()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        dir = new Vector3(h, 0, v);


        // ī�޶� ���� ������ �������� ���� �缳��
        dir = Camera.main.transform.TransformDirection(dir);
        dir.Normalize();

        // �����̽��ٸ� ������ �����Ŀ��� y �ӷ¿� �ִ´�.

        if (cc.isGrounded)
        {
            jumpCnt = 0;
            yVelocity = 0;
        }
        // ����Ƚ���� �ִ� ���� Ƚ������ ������
        if (jumpCnt < maxjumpcnt)
        {
            // �����Ŀ��� y�ӷ¿� �ִ´�.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpPower;
                // ����Ƚ���� ����
                jumpCnt++;
            }
        }
        // v = v0 + at
        //yVelocity += gravity * Time.deltaTime;

        dir.y = yVelocity;

        // �� �������� �����̰� �ʹ�
        cc.Move(dir * 5 * Time.deltaTime);
        // transform.position += dir * 5 * Time.deltaTime;
    }

    void Idle()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            state = PlayerState.Jump;
            anim.SetTrigger("Jump");
        }

        if (dir.magnitude > 1)
        {
            
            state = PlayerState.Run;
            anim.SetTrigger("Run");
        }

        //if (dir.magnitude > 0)
        //{
        //    state = PlayerState.Run;
        //    anim.SetTrigger("Run");
        //}
    }
    void Run()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            state = PlayerState.Jump;
            anim.SetTrigger("Jump");
        }

        if (dir.magnitude <= 0)
        {
            state = PlayerState.Idle;
            anim.SetTrigger("Idle");
        }


        if (dir.magnitude > 0)
        {
            state = PlayerState.Idle;
            anim.SetTrigger("Run");

        }
    }

    void Jump()
    {

        if (cc.isGrounded == true)
        {
            state = PlayerState.Idle;
            anim.SetTrigger("Idle");
        }
        if (Input.GetKeyDown(KeyCode.Space) && cc.isGrounded == false)
        {
            state = PlayerState.Fly;

        }
    }


    void Fly()
    {
        print("���ϻ��");
        state = PlayerState.Fly;
    }

    void Stern()
    {

    }



}
