using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class OJH_BattlePlayer : MonoBehaviourPun
{
    enum PcPlayerState
    {
        Idle,
        Run,
        Jump,
        Fly,
        Stern
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
    public bool rocketMode = false;
    public bool sternMode = false;

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
            playerRocket = otherRocket;
            playerFuel = otherFuel;
        }
        else
        {
            GameManager.instance.myPhotonView = photonView;
        }

        anim = GetComponentInChildren<Animator>();
        state = PcPlayerState.Idle;
        lr = GetComponent<LineRenderer>();
        RocketUI = GameObject.Find("RocketSlider");
        RocketFill = GameObject.Find("Fill");

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

        if (sternMode)
        {
            photonView.RPC("AniTrigger", RpcTarget.All, "Stern");
            return;
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
                    
                    break;
                default:
                    break;
            }

            if (RocUIAct == false)
            {
                RocketFill.GetComponent<Image>().color = Color.gray;
                // RocketUI.gameObject.transform.GetComponentInChildren<Image>().color = Color.gray;
                // RocketUI.transform.Find("Fill").GetComponent<Image>().color = Color.gray;
            }
            else
            {
                RocketFill.GetComponent<Image>().color = Color.red;
                // RocketUI.transform.Find("Fill").GetComponent<Image>().color = Color.red;
            }

            if (GameManager.instance.rocketCnt == 0)
            {
                RocUIAct = false;
            }
            JumpTime();
            PcPlayerMove();
            Flying();
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

        int layerMask = 1 << 8;

        layerMask = ~layerMask;

        if (rayTrigger)
        {
            rayTime += Time.deltaTime;
            // lr.enabled = true; // 끝에서 다시 끌 것
            // 눈에서 레이저 발사
            Ray ray = new Ray(raypoint.transform.position, raypoint.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, layerMask))
            {
                rayBoomPoint = hit;
                photonView.RPC("RpcVrRay", RpcTarget.All, raypoint.transform.position, hit.point);
                //lr.SetPosition(0, raypoint.transform.position);
                //lr.SetPosition(1, hit.point);
            }
            
            else
            {
                // 부딪힌 지점 없으면 오른손에서 몇미터 앞 까지만 라인 그려라
                photonView.RPC("RpcVrRay", RpcTarget.All, raypoint.transform.position, raypoint.transform.position + raypoint.transform.forward * 100);
                //lr.SetPosition(0, raypoint.transform.position);
                //lr.SetPosition(1, raypoint.transform.position + raypoint.transform.forward * 100);
            }
        }

        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger) && rayTime > 2f)
        {
            rayTime = 0;
            rayTrigger = false;
            GameObject rayBoom = PhotonNetwork.Instantiate("RayBoom", rayBoomPoint.point, Quaternion.identity);
            GameObject booom = PhotonNetwork.Instantiate("MagicExplosion", rayBoomPoint.point, Quaternion.identity);

            photonView.RPC("RpcVrRay", RpcTarget.All, raypoint.transform.position, raypoint.transform.position);
            //lr.SetPosition(0, raypoint.transform.position);
            //lr.SetPosition(1, raypoint.transform.position);
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger) && rayTime < 2f)
        {
            rayTime = 0;
            rayTrigger = false;
            photonView.RPC("RpcVrRay", RpcTarget.All, raypoint.transform.position, raypoint.transform.position);
            //lr.SetPosition(0, raypoint.transform.position);
            //lr.SetPosition(1, raypoint.transform.position);
        }
        
    }
    [PunRPC]
    void RpcVrRay(Vector3 startPos, Vector3 endPos)
    {
        lr.SetPosition(0, startPos);
        lr.SetPosition(1, endPos);
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
            yVelocity += gravity * 2/3 * Time.deltaTime;
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

                // 로켓 잔여량 ui 표시
                Slider slider = RocketUI.GetComponent<Slider>();
                slider.value = (RocketSetTime - currTime) / RocketSetTime;

                // 로켓 부스터 이팩트 넣어야함.
                if (currTime >= 5)
                {
                    GameManager.instance.RocketCount(-1);
                    photonView.RPC("MyRocketImg", RpcTarget.All, GameManager.instance.rocketCnt);
                    rocketMode = false;
                    currTime = 0;
                }
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                yVelocity = 0;
                rocketMode = false;
            }
        }

    }

    [PunRPC]
    public void MyRocketImg(int rockCnt)
    {
        playerRocket.GetComponent<MeshRenderer>().enabled = rockCnt > 0;
        //if (photonView.IsMine)
        //{
        //    if (GameManager.instance.rocketCnt == 0)
        //    {
        //        if (playerRocket.GetComponent<MeshRenderer>().enabled == true)
        //        {
        //            playerRocket.GetComponent<MeshRenderer>().enabled = false;
        //        }
        //    }
        //    if (GameManager.instance.rocketCnt > 0)
        //    {
        //        if (playerRocket.GetComponent<MeshRenderer>().enabled == false)
        //        {
        //            playerRocket.GetComponent<MeshRenderer>().enabled = true;
        //        }
        //    }
        //}
    }

    [PunRPC]
    public void RpcFuelOn()
    {
        playerFuel.GetComponent<MeshRenderer>().enabled = true;
    }

    [PunRPC]
    public void RpcFuelOff()
    {
        playerFuel.GetComponent<MeshRenderer>().enabled = false;
    }
    void Idle()
    {
        if (rocketMode)
        {
            state = PcPlayerState.Fly;
            photonView.RPC("AniTrigger", RpcTarget.All, "Fly");
            //anim.SetTrigger("Fly");
            return;
        }

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
        if (rocketMode == true)
        {
            state = PcPlayerState.Fly;
            photonView.RPC("AniTrigger", RpcTarget.All, "Fly");
            //anim.SetTrigger("Fly");
            return;
        }

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
        if (rocketMode == true)
        {
            state = PcPlayerState.Fly;
            photonView.RPC("AniTrigger", RpcTarget.All, "Fly");
            //anim.SetTrigger("Fly");
            return;
        }

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

    void Fly()
    {
        if (rocketMode == false)
        {
            state = PcPlayerState.Idle;
            photonView.RPC("AniTrigger", RpcTarget.All, "Idle");
            //anim.SetTrigger("Idle");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 14)
        {
            Destroy(other.gameObject);
            // 내가 먹었을 경우에만 로켓숫자를 늘려라
            if (photonView.IsMine)
            {
                RocUIAct = true;
                GameManager.instance.RocketCount(1);
                photonView.RPC("MyRocketImg", RpcTarget.All, GameManager.instance.rocketCnt);
            }
        }

        if (other.gameObject.layer == 15)
        {
            if (photonView.IsMine)
            {
                if (playerFuel.GetComponent<MeshRenderer>().enabled == false)
                {
                    // DestroyObj(other.gameObject.name);
                    photonView.RPC("RpcFuelOn", RpcTarget.All);
                }
            }
        }
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
        if (other.gameObject.layer == 16)
        {
            if (playerFuel.GetComponent<MeshRenderer>().enabled)
            {
                if (photonView.IsMine)
                {
                    fuelTime += Time.deltaTime;
                }
                if (fuelTime >= 3.3f)
                {
                    playerFuel.layer = 18;
                    photonView.RPC("RpcFuelOff", RpcTarget.All);
                    fuelTime = 0;
                    photonView.RPC("RpcFuelinput", RpcTarget.All);
                    //GameManager.instance.FuelCount(1);
                }
            }
        }
    }

    [PunRPC]
    void RpcFuelinput()
    {
        GameManager.instance.FuelCount(1);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 16)
        {
            fuelTime = 0;
        }

        if (other.gameObject.layer == 7)
        {
            // 벨로시티를 사용해서
            // 벨로시티 값이 작으면 곧장 sternMode 해제
            // 벨로시티 값이 크면 1초 후에 sternMode 해제
        }
    }

    public void SternReset2()
    {
        StartCoroutine(SternReset());
    }
    IEnumerator SternReset()
    {
        yield return new WaitForSeconds(1);
        sternMode = false;
        gameObject.GetComponent<CharacterController>().enabled = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        photonView.RPC("AniTrigger", RpcTarget.All, "Idle");
    }

    [PunRPC]
    public void AniTrigger(string state)
    {
        anim.SetTrigger(state);
    }
}

