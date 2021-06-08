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

    // �� VR��
    public Transform[] myBody;
    // �� VR ��
    public Transform[] otherBody;
    // PC �� ��ġ
    Vector3 pos;
    // PC �� ȸ����
    Quaternion rot;


    bool isVR;



    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //stream.SendNext(transform.position);
            // ���� PC �� ��
            if (isVR == false)
            {
                stream.SendNext(transform.position);
                stream.SendNext(transform.rotation);
            }

            // ���� VR �� ��
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
            // ������ PC�ϋ�
            if (isVR == false)
            {
                pos = (Vector3)stream.ReceiveNext();
                rot = (Quaternion)stream.ReceiveNext();
            }
            // ������ VR�϶�
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
            print("vr�� ���̾��");
            this.gameObject.layer = 8;
        }
        else
        {
            print("pc�� ���̾��");
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

        #region �� ĳ���� �� ����
        // ���� vr�̰�, ������ �����̶�� ������ �� Ȱ��ȭ�ϱ�
        if (isVR)
        {
            myVrModel.SetActive(photonView.IsMine);
        }
        // ���� pc�̰�, ������ �����̶�� ������ �� Ȱ��ȭ�ϱ�
        if (isVR == false)
        {
            myPcModel.SetActive(photonView.IsMine);
        }
        #endregion

        #region ���� �� ����
        // ������ ������ �ƴ��� Ȯ��
        if (photonView.IsMine == false)
        {
            // RpcisPresent�� ���� ���� vr�� pc ����
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
