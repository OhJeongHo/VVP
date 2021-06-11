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
        // 충돌한 놈이 레이어 11번인 경우 = pc플레이어인 경우
        if (other.gameObject.layer == 11)
        {
            // 알피씨 불가능. 포톤은 int나 float같은것들만 쏠 수 있고 아래처럼 큰 단위는 못보냄. 그래서 함수 거쳐야함.
            RingOut(other.GetComponent<PhotonView>().ViewID);

        }
    }

    public void RingOut(int viewId)
    {
        // 한참동안 작동이 안됐었는데, rpc를 쏘려면 게임오브젝트에 포톤뷰가 붙어있어야한다
        // 한단계를 거치면서 파라미터값을 int로 변환해서 쏜다.
        photonView.RPC("RpcRingOut", RpcTarget.All, viewId);
    }



    // rpc로 쏜다
    [PunRPC]
    void RpcRingOut(int viewId)
    {
        // 입력받은 viewid값을 게임매니저에 넣고 돌려서 나온 번호를 pcPlayer로 지정한다.
        pcPlayer = GameManager.instance.GetPhotonView(viewId);
        //  lt.pcplayer = other.gameObject.GetComponent<PhotonView>();

        // 일단 pcPlayer를 비활성화한다. 이 놈이 링아웃된 놈이니까.
        pcPlayer.gameObject.SetActive(false);
        //Destroy(pcPlayer.gameObject);

        // 아웃된 사람의 포톤 아이디를 리스트로 저장한다.

        // 내 포톤 아이디가 입력된 아이디 값과 동일한 경우
        if (GameManager.instance.myPhotonView.ViewID == viewId)
        {
            // 캠을 켜준다. 그래야 아웃된 놈만 카메라가 이동됨.
            cam.gameObject.SetActive(true);
            outUI.gameObject.SetActive(true);
        }

        // 코루틴함수써서 다시 부활하는 거 동기화 해준다.
        StartCoroutine(Respone(viewId, pcPlayer));
        // 그냥 했더니 pc플레이어가 충돌때마다 바뀌어서 동시에 여러명 아웃될 경우 마지막 사람만 부활함.
        // 리스트를 써서 담아두려고 했는데 그냥 파라미터로 같이 넘기는게 훨씬 더 간편한 방법임.
    }

    IEnumerator Respone(int viewId, PhotonView outPlayer)
    {
        yield return new WaitForSeconds(3f);

        if (GameManager.instance.myPhotonView.ViewID == viewId)
        {
            // 캠을 꺼준다. 그래야 아웃된 놈만 카메라가 이동됨.
            cam.gameObject.SetActive(false);
            outUI.gameObject.SetActive(false);
        }
        // 3초뒤에 오브젝트 다시 활성화 시켜서 부활장소로 옮긴다.
        // PhotonNetwork.Instantiate("BattlePlayer", apr.transform.position, Quaternion.Euler(0, 0, 0));
        // 이럴경우 로켓카운트를 초기화시켜야함

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
        //    print("충돌");
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
