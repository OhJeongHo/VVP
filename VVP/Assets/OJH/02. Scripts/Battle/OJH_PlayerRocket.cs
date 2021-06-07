using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OJH_PlayerRocket : MonoBehaviour
{
    public GameObject player;
    float speed = 10;
    Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        RocketMove();
    }

    

    void RocketMove()
    {
        if (player.GetComponent<LobbyActive>().rocketMode == false)// GameManager.instance.rocketCnt == 0)
        {
            return;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            // 로켓 부스터 이팩트 넣어야함.
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            

            dir = transform.forward * v + transform.right * h + transform.up;
            dir.Normalize();

            player.transform.position += dir * speed * Time.deltaTime;
            //transform.position += dir * 5 * Time.deltaTime;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        // 땅과 충돌
        if (other.gameObject.layer == 6)
        {
            player.GetComponent<LobbyActive>().rocketMode = false;
        }
    }
    
}
