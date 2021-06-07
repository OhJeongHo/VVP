using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OJH_PlayerRocket : MonoBehaviour
{
    public GameObject player;
    public GameObject fire;
    public GameObject rocketSound;
    CharacterController cc;
    float speed = 10;
    Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        cc = player.GetComponent<CharacterController>();
        GameManager.instance.playerRocket = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        RocketMove();
    }

    

    void RocketMove()
    {
        if (player.GetComponent<OJH_BattlePlayer>().rocketMode == false)// GameManager.instance.rocketCnt == 0)
        {
            if (fire.activeSelf)
            {
                fire.SetActive(false);
                rocketSound.SetActive(false);
            }
            return;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            fire.SetActive(true);
            rocketSound.SetActive(true);
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            

            dir = transform.forward * v + transform.right * h + Vector3.up;
            dir.Normalize();

            cc.Move(dir * speed * Time.deltaTime);
            // player.transform.position += dir * speed * Time.deltaTime;
            //transform.position += dir * 5 * Time.deltaTime;
        }
        
    }

    
}
