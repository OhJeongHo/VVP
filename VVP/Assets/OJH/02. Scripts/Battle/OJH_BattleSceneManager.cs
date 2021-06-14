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
        // �ѽð� ���ҽ���
        setTime -= Time.deltaTime;

        if (setTime >= 60f)
        {
            min = (int)setTime / 60;
            sec = setTime % 60;
            gameTime.text = "���� �ð� : " + min + "��" + (int)sec + "��";
        }
        if (setTime < 60f)
        {
            gameTime.text = "���� �ð� : " + (int)setTime + "��";
        }

        if (setTime <= 0)
        {
            gameTime.text = "���� �ð� : 0��";
            // ���ѽð� ������ vr �¸�
            VRWin = true;
        }

        if (VRWin)
        {
            // pc �й� ui �ߵ���
            pclose.SetActive(true);
            // vr �¸� ui �ߵ���
            if (GameManager.instance.isVR)
            {
                GameManager.instance.vrwin = true;
            }
            Invoke("Restart", 3f);
        }

        if (PCWin)
        {
            // pc �¸� ui �ߵ���
            pcwin.SetActive(true);
            // vr �й� ui �ߵ���
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
