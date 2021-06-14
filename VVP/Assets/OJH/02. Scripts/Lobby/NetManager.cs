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

    // �� ��� ĳ��
    Dictionary<string, RoomInfo> roomCache = new Dictionary<string, RoomInfo>();

    // Scrollview - content
    public Transform content;
    // RoomInfomation ��ư ����
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
        print("OnConnected / ����!");
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.NickName = "�÷��̾� " + Random.Range(0, 1000);

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("OnJoinedLobby / �κ�����!");

        // ���⼭�� �ν�źƼ����Ʈ�� �ƴ϶� ���� �÷��̾ �����ؾ���
        // �� �Ŀ� �Ʒ� �ڵ带 ��ư�� �־ ���� �����ؾ���.

        // PhotonNetwork.JoinOrCreateRoom("OJH", new RoomOptions(), TypedLobby.Default);
    }

    public void CreateRoom()
    {
        
        // �� �ɼ�
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

        // ���� �����
        PhotonNetwork.JoinOrCreateRoom(roomNameInput.text, roomOptions, TypedLobby.Default);

    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("OnCreatedRoom / �����!");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        print("�� ���� ����");
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(roomNameInput.text);
        print("Ŭ��!");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("OnJoinedRoom / ������!");
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

    // �� ���� ����
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("�� ���� ����");
    }

    // ���� �� ���� ����
    // ���ʿ��� ��ü �� ����Ʈ�� �ش�
    // �� ������ �߰�/������ �� ������ ���´�
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            print(roomList[i].Name);
            // roomCache.Add(roomList[i].Name, roomList[i]);
        }
        // ���� ������� UI�� ����
        DeleteRoomList();
        // roomCache ���� ����
        UpdateRoomCache(roomList);
        // UI ���Ӱ� �����

        CreatRoomList();
    }

    // roomCache ����
    void UpdateRoomCache(List<RoomInfo> roomList)
    {
        for(int i = 0; i < roomList.Count; i++)
        {
            // ���࿡ ���� �Ǵ� �߰��� ���� roomCache�� ������
            if (roomCache.ContainsKey(roomList[i].Name))
            {
                // ���࿡ �� ���� �������Ѵٸ�
                if (roomList[i].RemovedFromList)
                {
                    roomCache.Remove(roomList[i].Name);
                    continue;
                }
            }

            // ���� roommCache�� ���� �Ǵ� �߰�
            roomCache[roomList[i].Name] = roomList[i];
        }
    }

    // �� ���� ����
    void DeleteRoomList()
    {
        foreach(Transform tr in content)
        {
            Destroy(tr.gameObject);
        }
    }

    // ����������
    void CreatRoomList()
    {
        foreach(RoomInfo info in roomCache.Values)
        {
            // ���������̼� ��ư ���忡�� roominfo��ư ����
            GameObject room = Instantiate(roomInfoFactory, content);
            // ������� roominfo��ư�� content�� �ڽ����� ����
          
            // ������� roominfo��ư���� roominfo��ư ������Ʈ �����ͼ�
            RoomInfomation btn = room.GetComponent<RoomInfomation>();
            // ������ ������Ʈ�� setinfo�Լ� ȣ��

            int mode = (int)(info.CustomProperties["mode"]);
            btn.Setinfo(info.Name, info.PlayerCount, info.MaxPlayers, mode);           
        }
    }
}
