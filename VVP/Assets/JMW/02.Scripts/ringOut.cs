using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ringOut : MonoBehaviourPun
{
    public GameObject apr;
    public Camera cam;
    public PhotonView pcPlayer;
    public GameObject outUI;


    //List<GameObject> objs = new List<GameObject>();

    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ���� ���̾� 11���� ��� = pc�÷��̾��� ���
        if (other.gameObject.layer == 11)
        {
            // ���Ǿ� �Ұ���. ������ int�� float�����͵鸸 �� �� �ְ� �Ʒ�ó�� ū ������ ������. �׷��� �Լ� ���ľ���.
            RingOut(other.GetComponent<PhotonView>().ViewID);

        }
    }

    public void RingOut(int viewId)
    {
        // �������� �۵��� �ȵƾ��µ�, rpc�� ����� ���ӿ�����Ʈ�� ����䰡 �پ��־���Ѵ�
        // �Ѵܰ踦 ��ġ�鼭 �Ķ���Ͱ��� int�� ��ȯ�ؼ� ���.
        photonView.RPC("RpcRingOut", RpcTarget.All, viewId);
    }



    // rpc�� ���
    [PunRPC]
    void RpcRingOut(int viewId)
    {
        // �Է¹��� viewid���� ���ӸŴ����� �ְ� ������ ���� ��ȣ�� pcPlayer�� �����Ѵ�.
        pcPlayer = GameManager.instance.GetPhotonView(viewId);
        //  lt.pcplayer = other.gameObject.GetComponent<PhotonView>();

        // �ϴ� pcPlayer�� ��Ȱ��ȭ�Ѵ�. �� ���� ���ƿ��� ���̴ϱ�.
        pcPlayer.gameObject.SetActive(false);
        //Destroy(pcPlayer.gameObject);

        // �ƿ��� ����� ���� ���̵� ����Ʈ�� �����Ѵ�.

        // �� ���� ���̵� �Էµ� ���̵� ���� ������ ���
        if (GameManager.instance.myPhotonView.ViewID == viewId)
        {
            // ķ�� ���ش�. �׷��� �ƿ��� �� ī�޶� �̵���.
            cam.gameObject.SetActive(true);
            outUI.gameObject.SetActive(true);
        }

        // �ڷ�ƾ�Լ��Ἥ �ٽ� ��Ȱ�ϴ� �� ����ȭ ���ش�.
        StartCoroutine(Respone(viewId, pcPlayer));
        // �׳� �ߴ��� pc�÷��̾ �浹������ �ٲ� ���ÿ� ������ �ƿ��� ��� ������ ����� ��Ȱ��.
        // ����Ʈ�� �Ἥ ��Ƶη��� �ߴµ� �׳� �Ķ���ͷ� ���� �ѱ�°� �ξ� �� ������ �����.
    }

    IEnumerator Respone(int viewId, PhotonView outPlayer)
    {
        yield return new WaitForSeconds(3f);

        if (GameManager.instance.myPhotonView.ViewID == viewId)
        {
            // ķ�� ���ش�. �׷��� �ƿ��� �� ī�޶� �̵���.
            cam.gameObject.SetActive(false);
            outUI.gameObject.SetActive(false);
        }
        // 3�ʵڿ� ������Ʈ �ٽ� Ȱ��ȭ ���Ѽ� ��Ȱ��ҷ� �ű��.
        // PhotonNetwork.Instantiate("BattlePlayer", apr.transform.position, Quaternion.Euler(0, 0, 0));
        // �̷���� ����ī��Ʈ�� �ʱ�ȭ���Ѿ���

        outPlayer.gameObject.SetActive(true);
        outPlayer.gameObject.transform.position = apr.transform.position;
        if (outPlayer.gameObject.GetComponent<Rigidbody>().isKinematic == false)
        {
            outPlayer.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
        if (outPlayer.gameObject.GetComponent<CharacterController>().enabled == false)
        {
            outPlayer.gameObject.GetComponent<CharacterController>().enabled = true;
        }
        if (outPlayer.GetComponent<OJH_BattlePlayer>().sternMode == true)
        {
            outPlayer.GetComponent<OJH_BattlePlayer>().sternMode = false;
        }

        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.gameObject.layer == 11)
        //    {

        //    OJH_BattlePlayer pm = other.gameObject.GetComponent<OJH_BattlePlayer>();
        //    if(pm)
        //    {
        //        pm.enabled = false;
        //    }
        //    other.gameObject.transform.position = apr.transform.position;
        //    print("�浹");
        //    ScoreCnt.vrCnt += 1;

        //    StartCoroutine(Delay(pm));
        //    }
        //}

        //IEnumerator Delay(OJH_BattlePlayer pm)
        //{
        //    yield return new WaitForSeconds(0.1f);
        //    pm.enabled = true;
        //}

    }
}
