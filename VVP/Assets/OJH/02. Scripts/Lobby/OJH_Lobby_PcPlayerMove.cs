using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OJH_Lobby_PcPlayerMove : MonoBehaviour
{
    
    enum PcPlayerState
    {
        Idle,
        Run,
        Jump,
        Fly,
        Stern
    }


    Vector3 dir;
    Vector3 forward;
    public float jumpCnt;
    public float maxjumpcnt = 1;
    public float jumpPower = 2;
    float gravity = -9.8f;
    CharacterController cc;
    float yVelocity;
    float currTime;
    PcPlayerState state;
    Animator anim;
    PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        state = PcPlayerState.Idle;
        photonView = GetComponentInParent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine == false) return;

        
        switch (state)
        {
            case PcPlayerState.Idle:
                Idle();
                break;
            case PcPlayerState.Run:
                Run();
                break;
            case PcPlayerState.Jump:
                Jump();
                break;
            case PcPlayerState.Fly:
                Jump();
                break;
            case PcPlayerState.Stern:
                Jump();
                break;
            default:
                break;
        }
        PcPlayerMove();
    }

    void Idle()
    {
        Vector3 moveDir = new Vector3(dir.x, 0,  dir.z);
        if (moveDir.magnitude > 0 && !Input.GetButtonDown("Jump"))
        {
            state = PcPlayerState.Run;
            anim.SetTrigger("Run");
        }
        if (Input.GetButtonDown("Jump"))
        {
            state = PcPlayerState.Jump;
            anim.SetTrigger("Jump");
        }
    }

    void Run()
    {
        Vector3 moveDir = new Vector3(dir.x, 0, dir.z);
        if (moveDir.magnitude == 0 && !Input.GetButtonDown("Jump"))
        {
            state = PcPlayerState.Idle;
            anim.SetTrigger("Idle");
        }
        if (Input.GetButtonDown("Jump"))
        {
            state = PcPlayerState.Jump;
            anim.SetTrigger("Jump");
        }

    }

    void Jump()
    {
        currTime += Time.deltaTime;
        if (cc.isGrounded && currTime >= 0.1f)
        {
                state = PcPlayerState.Idle;
                anim.SetTrigger("Idle");
            currTime = 0;
        }
    }

    void PcPlayerMove()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        dir =  transform.forward * v;//  new Vector3(h, 0, v); //transform.right * h +
        dir.Normalize();

        //forward = new Vector3();
        //// ī�޶� ���� ������ �������� ���� �缳��
        //forward = Camera.main.transform.TransformDirection(forward);
        //forward.Normalize();

        transform.Rotate(0, Camera.main.transform.rotation.y, 0);

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
            if (Input.GetButtonDown("Jump"))
            {
                yVelocity = jumpPower;
                // ����Ƚ���� ����
                jumpCnt++;
            }
        }

        if (cc.isGrounded == false)
        {
            // v = v0 + at
            yVelocity += gravity * Time.deltaTime;

            dir.y = yVelocity;
        }

        // �� �������� �����̰� �ʹ�
        cc.Move(dir * 5 * Time.deltaTime);
        // transform.position += dir * 5 * Time.deltaTime;
    }
}
