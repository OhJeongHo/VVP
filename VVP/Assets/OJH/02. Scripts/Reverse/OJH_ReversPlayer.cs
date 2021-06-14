using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class OJH_ReversPlayer : MonoBehaviourPun
{
    enum PcPlayerState
    {
        Idle,
        Run,
        Jump,

    }


    public GameObject pccam;
    bool rayTrigger;

    LineRenderer lr;

    float laserTime;

    Vector3 dir;
    public CharacterController cc;
    float yVelocity;
    public float jumpCnt;
    public float jumpTime;
    bool jumpbool = false;
    public float maxjumpCnt = 1;
    public float jumpPower = 2;
    float gravity = -9.8f;
    float currTime;
    float rayTime;
    public int fuel;
    float fuelTime;

    PcPlayerState state;
    public Animator anim;

    GameObject RocketUI;
    GameObject RocketFill;
    float RocketSetTime = 5;
    bool RocUIAct = false;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.players.Add(photonView);
        if (photonView.IsMine == false)
        {

        }
        else
        {
            GameManager.instance.myPhotonView = photonView;
        }

        anim = GetComponentInChildren<Animator>();
        state = PcPlayerState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine == false)
        {
            return;
        }

        if (GameManager.instance.isVR)
        {
            VrPlayerMove();
        }

        if (GameManager.instance.isVR == false)
        {
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
                default:
                    break;
            }

            JumpTime();
            PcPlayerMove();
        }
    }


    void VrPlayerMove()
    {
        

    }
    

    void PcPlayerMove()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        dir = transform.forward * v + transform.right * h;
        dir.Normalize();

        transform.Rotate(0, pccam.transform.rotation.y, 0);

        if (cc.isGrounded)
        {
            jumpCnt = 0;
            yVelocity = 0;
        }

        if (Input.GetButtonDown("Jump") && jumpTime == 0)
        {
            if (jumpCnt < maxjumpCnt)
            {
                yVelocity = jumpPower;
                jumpCnt++;
                jumpbool = true;
            }
        }

        if (cc.isGrounded == false)
        {
            yVelocity += gravity * 2 / 3 * Time.deltaTime;
            dir.y = yVelocity;
        }

        cc.Move(dir * 5 * Time.deltaTime);
    }

    void JumpTime()
    {
        if (jumpbool)
        {
            jumpTime += Time.deltaTime;
            if (jumpTime > 1)
            {
                jumpTime = 0;
                jumpbool = false;
            }
        }
    }

    void Idle()
    {
        Vector3 moveDir = new Vector3(dir.x, 0, dir.z);
        if (moveDir.magnitude > 0 && !Input.GetButtonDown("Jump"))
        {
            state = PcPlayerState.Run;
            photonView.RPC("AniTrigger", RpcTarget.All, "Run");
            //anim.SetTrigger("Run");
        }
        if (Input.GetButtonDown("Jump") && jumpTime == 0)
        {
            state = PcPlayerState.Jump;
            photonView.RPC("AniTrigger", RpcTarget.All, "Jump");
            //anim.SetTrigger("Jump");
        }

    }

    void Run()
    {
        Vector3 moveDir = new Vector3(dir.x, 0, dir.z);
        if (moveDir.magnitude == 0 && !Input.GetButtonDown("Jump"))
        {
            state = PcPlayerState.Idle;
            photonView.RPC("AniTrigger", RpcTarget.All, "Idle");
            //anim.SetTrigger("Idle");
        }
        if (Input.GetButtonDown("Jump") && jumpTime == 0)
        {
            state = PcPlayerState.Jump;
            photonView.RPC("AniTrigger", RpcTarget.All, "Jump");
            //anim.SetTrigger("Jump");
        }
    }

    void Jump()
    {
        if (cc.isGrounded)
        {
            Vector3 moveDir = new Vector3(dir.x, 0, dir.z);
            if (moveDir.magnitude == 0)
            {
                state = PcPlayerState.Idle;
                photonView.RPC("AniTrigger", RpcTarget.All, "Idle");
                //anim.SetTrigger("Idle");
            }
            else
            {
                state = PcPlayerState.Run;
                photonView.RPC("AniTrigger", RpcTarget.All, "Run");
                //anim.SetTrigger("Run");
            }
        }
    }


    [PunRPC]
    public void AniTrigger(string state)
    {
        anim.SetTrigger(state);
    }
}