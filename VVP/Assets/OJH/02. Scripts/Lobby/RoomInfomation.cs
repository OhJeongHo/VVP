using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomInfomation : MonoBehaviour
{
    // 정보 보여줄 텍스트
    public Text info;

    // 방제목
    string room;

    // 정보 세팅
    public void Setinfo(string roomName, int currPlayer, int maxPlayer)
    {
        // 방제목 저장
        room = roomName;

        // 방제목 ( 현재인원 / 최대인원 )
        info.text = "[결투모드] : " + roomName + " ( " + currPlayer + " / " + maxPlayer + " )";
    }

    public void OnClick()
    {
        // NetManager(GameObject) 찾자
        GameObject go = GameObject.Find("NetManager");
        // NetManager(컴포넌트) 찾자
        NetManager nm = go.GetComponent<NetManager>();
        // roomNameInput.text에 방제목을 넣자
        nm.roomNameInput.text = room;
    }
}
