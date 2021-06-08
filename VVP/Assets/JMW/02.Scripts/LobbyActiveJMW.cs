using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyActiveJMW : MonoBehaviour
{
    //public LaserTurret Lt;

    public bool PlayerCtrl;
    GameObject pcCam;

    enum PcPlayerState
    {
        Idle,
        Run,
        Jump,
        Fly,
        Stern
    }

    

    public bool rocketMode = false;
    public bool sternMode = false;
    Vector3 dir;
    CharacterController cc;
    float yVelocity;
    public float jumpCnt;
    public float jumpTime;
    bool jumpbool = false;
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
                    Fly();
                    break;
                case PcPlayerState.Stern:
                    Jump();
                    break;
                default:
                    break;
            }

            JumpTime();
            PcPlayerMove();
            Flying();
            // RocketTest();
        }

        if (PlayerCtrl == true)
        {
            PcPlayerMove();
            Camera pcCam = GetComponent<Camera>();
            pcCam.enabled = true;
            
        }
    }

    void RocketTest()
    {
        if (rocketMode)
        {
            print("로켓모드 On");
        }
        else
        {
            print("로켓모드 Off");
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
        
        if (rocketMode == true)
        {
            return;
        }

        

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
            if (Input.GetButtonDown("Jump") && jumpTime == 0)
            {
                yVelocity = jumpPower;
                jumpCnt++;
                jumpbool = true;
            }
        }

        if (cc.isGrounded == false)
        {
            yVelocity += gravity * Time.deltaTime;
            dir.y = yVelocity;
        }

        cc.Move(dir * 15 * Time.deltaTime);
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

    void Flying()
    {
        if (GameManager.instance.rocketCnt == 0)
        {
            rocketMode = false;
            return;
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rocketMode = true;

                currTime += Time.deltaTime;
                // 로켓 부스터 이팩트 넣어야함.
                if (currTime >= 5)
                {
                    GameManager.instance.rocketCnt--;
                    rocketMode = false;
                }
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                rocketMode = false;
            }
        }
        
    }
    void Idle()
    {
        if (rocketMode)
        {
            state = PcPlayerState.Fly;
            anim.SetTrigger("Fly");
            return;
        }

        Vector3 moveDir = new Vector3(dir.x, 0, dir.z);
        if (moveDir.magnitude > 0 && !Input.GetButtonDown("Jump"))
        {
            state = PcPlayerState.Run;
            anim.SetTrigger("Run");
        }
        if (Input.GetButtonDown("Jump") && jumpTime == 0)
        {
            state = PcPlayerState.Jump;
            anim.SetTrigger("Jump");
        }
        
    }

    void Run()
    {
        if (rocketMode == true)
        {
            state = PcPlayerState.Fly;
            anim.SetTrigger("Fly");
            return;
        }

        Vector3 moveDir = new Vector3(dir.x, 0, dir.z);
        if (moveDir.magnitude == 0 && !Input.GetButtonDown("Jump"))
        {
            state = PcPlayerState.Idle;
            anim.SetTrigger("Idle");
        }
        if (Input.GetButtonDown("Jump") && jumpTime == 0)
        {
            state = PcPlayerState.Jump;
            anim.SetTrigger("Jump");
        }
    }

    void Jump()
    {
        if (rocketMode == true)
        {
            state = PcPlayerState.Fly;
            anim.SetTrigger("Fly");
            return;
        }

        if (cc.isGrounded)
        {
            Vector3 moveDir = new Vector3(dir.x, 0, dir.z);
            if (moveDir.magnitude == 0)
            {
                state = PcPlayerState.Idle;
                anim.SetTrigger("Idle");
            }
            else
            {
                state = PcPlayerState.Run;
                anim.SetTrigger("Run");
            }
        }
    }

    void Fly()
    {
        if (rocketMode == false)
        {
            state = PcPlayerState.Idle;
            anim.SetTrigger("Idle");
        }
    }

    //public void ff()
    //{
    //    PlayerCtrl = false;
    //}


    private void OnTriggerEnter(Collider other)
    {
        
    }
   
}
