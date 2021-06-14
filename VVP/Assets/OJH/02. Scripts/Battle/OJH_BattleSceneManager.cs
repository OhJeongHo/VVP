using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class OJH_BattleSceneManager : MonoBehaviourPunCallbacks
{
    public Transform vrpoint;
    public Transform pcpoint;
    
    public Text gameTime;

    float setTime = 180;
    int min;
    float sec;
    float currTime;


    bool VRWin = false;
    public GameObject pclose;
    bool PCWin = false;
    public GameObject pcwin;
    
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.isVR)
        {
            PhotonNetwork.Instantiate("BattlePlayer", vrpoint.position, Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate("BattlePlayer", pcpoint.position, Quaternion.Euler(0, 180, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 총시간 감소시작
        setTime -= Time.deltaTime;

        if (setTime >= 60f)
        {
            min = (int)setTime / 60;
            sec = setTime % 60;
            gameTime.text = "남은 시간 : " + min + "분" + (int)sec + "초";
        }
        if (setTime < 60f)
        {
            gameTime.text = "남은 시간 : " + (int)setTime + "초";
        }

        if (setTime <= 0)
        {
            gameTime.text = "남은 시간 : 0초";
            // 제한시간 끝나면 vr 승리
            VRWin = true;
        }

        if (VRWin)
        {
            // pc 패배 ui 뜨도록
            pclose.SetActive(true);
            // vr 승리 ui 뜨도록
            if (GameManager.instance.isVR)
            {
                GameManager.instance.vrwin = true;
            }
            Invoke("Restart", 3f);
        }

        if (PCWin)
        {
            // pc 승리 ui 뜨도록
            pcwin.SetActive(true);
            // vr 패배 ui 뜨도록
            if (GameManager.instance.isVR)
            {
                GameManager.instance.vrlose = true;
            }
        }

        if (GameManager.instance.vrlose)
        {
            pcwin.SetActive(true);
            if (GameManager.instance.isVR)
            {
                GameManager.instance.vrlose = true;
            }
        }
    }

    void Restart()
    {
        SceneManager.LoadScene("OJH_LobbyScene");
    }
}
