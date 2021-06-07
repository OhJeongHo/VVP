using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerM : MonoBehaviour
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
    CharacterController cc;
    float yVelocity;
    public float jumpCnt;
    public float maxjumpCnt = 1;
    public float jumpPower = 2;
    float gravity = -9.8f;
    float currTime;

    PcPlayerState state;
    Animator anim;

    public GameObject myVrModel;
    public GameObject myPcModel;

    bool isVR;

    // Start is called before the first frame update
    void Start()
    {
        isVR = GameManager.instance.isVR;
        cc = GetComponent<CharacterController>();

        if (isVR)
        {
            myVrModel.SetActive(true);
        }
        if (isVR == false)
        {
            myPcModel.SetActive(true);
            anim = GetComponentInChildren<Animator>();
            state = PcPlayerState.Idle;
        }


    }

    // Update is called once per frame
    void Update()
    {
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
    }

    void VrPlayerMove()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        dir = transform.forward * v + transform.right * h;
        dir.Normalize();
        print(dir);

        cc.Move(dir * 2 * Time.deltaTime);

        //if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        //{
        //    transform.position += transform.forward * 1;
        //}

        Vector2 joyStickR = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);
        if (joyStickR.magnitude > 0)
        {
            print(joyStickR.x + ",  " + joyStickR.y);
        }
        transform.Rotate(0, joyStickR.x * 70 * Time.deltaTime, 0);
    }


    void PcPlayerMove()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        dir = transform.forward * v + transform.right * h;
        dir.Normalize();

        transform.Rotate(0, Camera.main.transform.rotation.y, 0);

        if (cc.isGrounded)
        {
            jumpCnt = 0;
            yVelocity = 0;
        }

        if (jumpCnt < maxjumpCnt)
        {
            if (Input.GetButtonDown("Jump"))
            {
                yVelocity = jumpPower;
                jumpCnt++;
            }
        }

        if (cc.isGrounded == false)
        {
            yVelocity += gravity * Time.deltaTime;
            dir.y = yVelocity;
        }

        cc.Move(dir * 5 * Time.deltaTime);
    }

    void Idle()
    {
        Vector3 moveDir = new Vector3(dir.x, 0, dir.z);
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
}
