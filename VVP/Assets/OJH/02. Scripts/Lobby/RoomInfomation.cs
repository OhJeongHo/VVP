using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomInfomation : MonoBehaviour
{
    // ���� ������ �ؽ�Ʈ
    public Text info;
    // GameObject mode1, mode2, mode3;

    // ������
    string room;

    private void Start()
    {
        
        //mode1 = GameObject.Find("BattleMode");
        //mode2 = GameObject.Find("BattleModeR");
        //mode3 = GameObject.Find("Togetger");
    }

    // ���� ����
    public void Setinfo(string roomName, int currPlayer, int maxPlayer, int n)
    {
        // ������ ����
        room = roomName;

        //// ������ ( �����ο� / �ִ��ο� )
        //info.text = "[��Ʋ���] : " + roomName + " ( " + currPlayer + " / " + maxPlayer + " )";

        if (n == 1)
        {
            // ������ ( �����ο� / �ִ��ο� )
            info.text = "[��Ʋ���] : " + roomName + " ( " + currPlayer + " / " + maxPlayer + " )";
        }
        if (n == 2)
        {
            // ������ ( �����ο� / �ִ��ο� )
            info.text = "[��Ʋ���R] : " + roomName + " ( " + currPlayer + " / " + maxPlayer + " )";
        }
        if (n == 3)
        {
            // ������ ( �����ο� / �ִ��ο� )
            info.text = "[�������] : " + roomName + " ( " + currPlayer + " / " + maxPlayer + " )";
        }
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
