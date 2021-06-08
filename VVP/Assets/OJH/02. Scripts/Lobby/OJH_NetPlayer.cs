using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OJH_NetPlayer : MonoBehaviourPun, IPunObservable
{
    public GameObject myVrModel;
    public GameObject myPcModel;
    public GameObject otherVrModel;
    public GameObject otherPcModel;

    // 내 VR몸
    public Transform[] myBody;
    // 네 VR 몸
    public Transform[] otherBody;
    // PC 몸 위치
    Vector3 pos;
    // PC 몸 회전값
    Quaternion rot;


    bool isVR;



    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //stream.SendNext(transform.position);
            // 내가 PC 일 때
            if (isVR == false)
            {
                stream.SendNext(transform.position);
                stream.SendNext(transform.rotation);
            }

            // 내가 VR 일 때
            if (isVR)
            {
                for (int i = 0; i < myBody.Length; i++)
                {
                    stream.SendNext(myBody[i].position);
                    stream.SendNext(myBody[i].rotation);
                }
            }
        }

        if (stream.IsReading)
        {
            // pos = (Vector3)stream.ReceiveNext();
            // 상대방이 PC일떄
            if (isVR == false)
            {
                pos = (Vector3)stream.ReceiveNext();
                rot = (Quaternion)stream.ReceiveNext();
            }
            // 상대방이 VR일때
            if (isVR)
            {
                for (int i = 0; i < otherBody.Length; i++)
                {
                    otherBody[i].position = (Vector3)stream.ReceiveNext();
                    otherBody[i].rotation = (Quaternion)stream.ReceiveNext();
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(photonView.IsMine)
        {
            isVR = GameManager.instance.isVR;
            photonView.RPC("SendIsVR", RpcTarget.AllBuffered, isVR);
        }
        if (isVR)
        {
            print("vr로 레이어변경");
            this.gameObject.layer = 8;
        }
        else
        {
            print("pc로 레이어변경");
            this.gameObject.layer = 11;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine == false)
        {
            transform.position = Vector3.Lerp(transform.position, pos, 0.2f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, 0.2f);
        }
    }

    [PunRPC]
    void SendIsVR(bool b)
    {
        isVR = b;

        #region 내 캐릭터 모델 구현
        // 내가 vr이고, 포톤이 내것이라면 적절한 모델 활성화하기
        if (isVR)
        {
            myVrModel.SetActive(photonView.IsMine);
        }
        // 내가 pc이고, 포톤이 내것이라면 적절한 모델 활성화하기
        if (isVR == false)
        {
            myPcModel.SetActive(photonView.IsMine);
        }
        #endregion

        #region 상대방 모델 구현
        // 포톤이 내것이 아닌지 확인
        if (photonView.IsMine == false)
        {
            // RpcisPresent의 값에 따라서 vr과 pc 구별
            if (isVR)
            {
                otherVrModel.SetActive(true);
            }
            else
            {
                otherPcModel.SetActive(true);
            }
        }
        #endregion


        //OJH_BattlePlayer bp = GetComponent<OJH_BattlePlayer>();
        //if(bp)
        //{
        //    bp.Init();
        //}
    }
}
