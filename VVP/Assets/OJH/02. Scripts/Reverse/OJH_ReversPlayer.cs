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
        Stern
    }

    public GameObject raypoint;
    public RaycastHit rayBoomPoint;
    bool rayTrigger;

    LineRenderer lr;

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

    PcPlayerState state;
    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.players.Add(photonView);
        if (photonView.IsMine)
        {
            GameManager.instance.myPhotonView = photonView;
        }
        anim = GetComponentInChildren<Animator>();
        state = PcPlayerState.Idle;

        lr = GetComponent<LineRenderer>();
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
                case PcPlayerState.Stern:

                    break;
                default:
                    break;
            }
        }
    }

    private void Jump()
    {
    }

    private void Run()
    {

    }

    private void Idle()
    {

    }

    void VrPlayerMove()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        dir = transform.forward * v + transform.right * h;
        dir.Normalize();
        // print(dir);

        cc.Move(dir * 2 * Time.deltaTime);

        Vector2 joyStickR = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);
        if (joyStickR.magnitude > 0)
        {
            print(joyStickR.x + ",  " + joyStickR.y);
        }
        transform.Rotate(0, joyStickR.x * 70 * Time.deltaTime, 0);

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
            }

            else
            {
                // 부딪힌 지점 없으면 오른손에서 몇미터 앞 까지만 라인 그려라
                photonView.RPC("RpcVrRay", RpcTarget.All, raypoint.transform.position, raypoint.transform.position + raypoint.transform.forward * 100);
            }
        }

        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger) && rayTime > 2f)
        {
            rayTime = 0;
            rayTrigger = false;
            GameObject rayBoom = PhotonNetwork.Instantiate("RayBoom", rayBoomPoint.point, Quaternion.identity);
            GameObject booom = PhotonNetwork.Instantiate("MagicExplosion", rayBoomPoint.point, Quaternion.identity);

            photonView.RPC("RpcVrRay", RpcTarget.All, raypoint.transform.position, raypoint.transform.position);
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger) && rayTime < 2f)
        {
            rayTime = 0;
            rayTrigger = false;
            photonView.RPC("RpcVrRay", RpcTarget.All, raypoint.transform.position, raypoint.transform.position);
        }

    }
    [PunRPC]
    void RpcVrRay(Vector3 startPos, Vector3 endPos)
    {
        lr.SetPosition(0, startPos);
        lr.SetPosition(1, endPos);
    }
}
