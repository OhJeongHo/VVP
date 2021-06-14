using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class togetherSc : MonoBehaviourPun
{
    enum PcPlayerState
    {
        Idle,
        Run,
        Jump,
        
    }

    public Camera pccam;
    public GameObject playerRocket;
    public GameObject otherRocket;
    public GameObject playerFuel;
    public GameObject otherFuel;
    public GameObject raypoint;
    public RaycastHit rayBoomPoint;
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

    public GameObject Mask;




    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.players.Add(photonView);
        if (photonView.IsMine == false)
        {
            playerRocket = otherRocket;
            playerFuel = otherFuel;
        }
        else
        {
            GameManager.instance.myPhotonView = photonView;
        }

        anim = GetComponentInChildren<Animator>();
        state = PcPlayerState.Idle;
    
     

        if (gameObject.layer == 8)
        {
            GameObject.Find("Tur1 (2)").GetComponentInChildren<Tower>().target = Mask.transform;
        }

        //if (GameManager.instance.isVR)
        //{
        //    // 손 애니메이션
        //    //anim = pcanim;
        //    //state = PcPlayerState.Idle;
        //}

    }

    //public void Init()
    //{
    //    anim = GetComponentInChildren<Animator>();
    //}

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
        if (GameManager.instance.vrClose)
        {
            return;
        }
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        {
            print("버튼 누름");
            rayTrigger = true;
        }


       

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

    


    private void OnTriggerEnter(Collider other)
    {
        
    }

    //public void DestroyObj(string name)
    //{
    //    photonView.RPC("RpcDestroy", RpcTarget.All, name);
    //}
    //[PunRPC]
    //void RpcDestroy(string name)
    //{
    //    GameObject bye = GameObject.Find(name);
    //    Destroy(bye);
    //}

    private void OnTriggerStay(Collider other)
    {
       
    }

  

 

    [PunRPC]
    public void AniTrigger(string state)
    {
        anim.SetTrigger(state);
    }
}

