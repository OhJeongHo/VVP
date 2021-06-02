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


    //상태를담을변수
    PlayerState state;

    // 캐릭터 컨트롤
    CharacterController cc;
    // 애니메이터
    Animator anim;
    // 중력
    //float gravity = -9.8f;
    // 점프 파워
    float jumpPower = 3;
    // y 속력
    float yVelocity;
    // 점프 횟수
    int jumpCnt = 0;
    // 최대 점프 횟수
    int maxjumpcnt = 1;

    // 이동 벡터
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


        // 카메라가 보는 방향을 기준으로 방향 재설정
        dir = Camera.main.transform.TransformDirection(dir);
        dir.Normalize();

        // 스페이스바를 누르면 점프파워를 y 속력에 넣는다.

        if (cc.isGrounded)
        {
            jumpCnt = 0;
            yVelocity = 0;
        }
        // 점프횟수가 최대 점프 횟수보다 작으면
        if (jumpCnt < maxjumpcnt)
        {
            // 점프파워를 y속력에 넣는다.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpPower;
                // 점프횟수를 증가
                jumpCnt++;
            }
        }
        // v = v0 + at
        //yVelocity += gravity * Time.deltaTime;

        dir.y = yVelocity;

        // 그 방향으로 움직이고 싶다
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
        print("로켓사용");
        state = PlayerState.Fly;
    }

    void Stern()
    {

    }



}
