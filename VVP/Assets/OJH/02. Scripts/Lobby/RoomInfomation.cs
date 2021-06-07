using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomInfomation : MonoBehaviour
{
    // ���� ������ �ؽ�Ʈ
    public Text info;

    // ������
    string room;

    // ���� ����
    public void Setinfo(string roomName, int currPlayer, int maxPlayer)
    {
        // ������ ����
        room = roomName;

        // ������ ( �����ο� / �ִ��ο� )
        info.text = "[�������] : " + roomName + " ( " + currPlayer + " / " + maxPlayer + " )";
    }

    public void OnClick()
    {
        // NetManager(GameObject) ã��
        GameObject go = GameObject.Find("NetManager");
        // NetManager(������Ʈ) ã��
        NetManager nm = go.GetComponent<NetManager>();
        // roomNameInput.text�� �������� ����
        nm.roomNameInput.text = room;
    }
}
