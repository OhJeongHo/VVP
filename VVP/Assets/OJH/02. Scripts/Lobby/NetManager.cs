using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetManager : MonoBehaviourPunCallbacks
{
    public InputField roomNameInput;
    public InputField maxUserInput;
    public GameObject mode1, mode2, mode3;

    // 방 목록 캐시
    Dictionary<string, RoomInfo> roomCache = new Dictionary<string, RoomInfo>();

    // Scrollview - content
    public Transform content;
    // RoomInfomation 버튼 공장
    public GameObject roomInfoFactory;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
        //Unity.XR.Oculus.OculusLoader.
    }

    // Update is called once per frame
    void Update()
    {

    }


    public override void OnConnected()
    {
        base.OnConnected();
        print("OnConnected / 접속!");
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.NickName = "플레이어 " + Random.Range(0, 1000);

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("OnJoinedLobby / 로비접속!");

        // 여기서는 인스탄티에이트가 아니라 따로 플레이어가 존재해야함
        // 그 후에 아래 코드를 버튼에 넣어서 방을 생성해야함.

        // PhotonNetwork.JoinOrCreateRoom("OJH", new RoomOptions(), TypedLobby.Default);
    }

    public void CreateRoom()
    {
        
        // 방 옵션
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = byte.Parse(maxUserInput.text);

        string[] keys = { "mode"};
        roomOptions.CustomRoomPropertiesForLobby = keys;

        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();

        int mode = 0;
        if (mode1.activeSelf)
        {
            mode = 1;
        }
        if (mode2.activeSelf)
        {
            mode = 2;
        }
        if (mode3.activeSelf)
        {
            mode = 3;
        }
        hash.Add("mode", mode);
        roomOptions.CustomRoomProperties = hash;

        // 방을 만든다
        PhotonNetwork.JoinOrCreateRoom(roomNameInput.text, roomOptions, TypedLobby.Default);

    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("OnCreatedRoom / 방생성!");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        print("방 생성 실패");
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(roomNameInput.text);
        print("클릭!");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("OnJoinedRoom / 방접속!");
        if (mode1.activeSelf)
        {
            PhotonNetwork.LoadLevel("OJH_RoomScene");
        }
        if (mode2.activeSelf)
        {
            PhotonNetwork.LoadLevel("OJH_ReverseRoom");
        }
        if (mode3.activeSelf)
        {
            PhotonNetwork.LoadLevel("OJH_TogetherRoom");
        }
    }

    // 방 접속 실패
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("방 접속 실패");
    }

    // 현재 방 정보 갱신
    // 최초에는 전체 방 리스트를 준다
    // 그 다음은 추가/삭제된 방 정보만 들어온다
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            print(roomList[i].Name);
            // roomCache.Add(roomList[i].Name, roomList[i]);
        }
        // 현재 만들어진 UI를 삭제
        DeleteRoomList();
        // roomCache 정보 갱신
        UpdateRoomCache(roomList);
        // UI 새롭게 만든다

        CreatRoomList();
    }

    // roomCache 갱신
    void UpdateRoomCache(List<RoomInfo> roomList)
    {
        for(int i = 0; i < roomList.Count; i++)
        {
            // 만약에 변경 또는 추가된 방이 roomCache에 있으면
            if (roomCache.ContainsKey(roomList[i].Name))
            {
                // 만약에 그 방을 지워야한다면
                if (roomList[i].RemovedFromList)
                {
                    roomCache.Remove(roomList[i].Name);
                    continue;
                }
            }

            // 방을 roommCache에 변경 또는 추가
            roomCache[roomList[i].Name] = roomList[i];
        }
    }

    // 방 정보 삭제
    void DeleteRoomList()
    {
        foreach(Transform tr in content)
        {
            Destroy(tr.gameObject);
        }
    }

    // 방정보생성
    void CreatRoomList()
    {
        foreach(RoomInfo info in roomCache.Values)
        {
            // 룸인포메이션 버튼 공장에서 roominfo버튼 생성
            GameObject room = Instantiate(roomInfoFactory, content);
            // 만들어진 roominfo버튼을 content의 자식으로 세팅
          
            // 만들어진 roominfo버튼에서 roominfo버튼 컴포넌트 가져와서
            RoomInfomation btn = room.GetComponent<RoomInfomation>();
            // 가져온 컴포넌트의 setinfo함수 호출

            int mode = (int)(info.CustomProperties["mode"]);
            btn.Setinfo(info.Name, info.PlayerCount, info.MaxPlayers, mode);           
        }
    }
}
