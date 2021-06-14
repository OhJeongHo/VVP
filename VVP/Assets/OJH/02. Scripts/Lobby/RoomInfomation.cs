using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomInfomation : MonoBehaviour
{
    // 정보 보여줄 텍스트
    public Text info;
    // GameObject mode1, mode2, mode3;

    // 방제목
    string room;

    private void Start()
    {
        
        //mode1 = GameObject.Find("BattleMode");
        //mode2 = GameObject.Find("BattleModeR");
        //mode3 = GameObject.Find("Togetger");
    }

    // 정보 세팅
    public void Setinfo(string roomName, int currPlayer, int maxPlayer, int n)
    {
        // 방제목 저장
        room = roomName;

        //// 방제목 ( 현재인원 / 최대인원 )
        //info.text = "[배틀모드] : " + roomName + " ( " + currPlayer + " / " + maxPlayer + " )";

        if (n == 1)
        {
            // 방제목 ( 현재인원 / 최대인원 )
            info.text = "[배틀모드] : " + roomName + " ( " + currPlayer + " / " + maxPlayer + " )";
        }
        if (n == 2)
        {
            // 방제목 ( 현재인원 / 최대인원 )
            info.text = "[배틀모드R] : " + roomName + " ( " + currPlayer + " / " + maxPlayer + " )";
        }
        if (n == 3)
        {
            // 방제목 ( 현재인원 / 최대인원 )
            info.text = "[협동모드] : " + roomName + " ( " + currPlayer + " / " + maxPlayer + " )";
        }
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
