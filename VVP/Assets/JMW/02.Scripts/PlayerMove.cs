using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CharacterController cc;

    float gravity = -9.8f;

    float jumpPower = 3;

    float yVelocity;

    int jumpCnt = 0;

    int maxJumpCnt = 1;

    public float speed = 5;




    void Start()
    {
        cc = GetComponent<CharacterController>();
    }


    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);

        dir = Camera.main.transform.TransformDirection(dir);
        dir.Normalize();


        if (cc.isGrounded == true)
        {
            jumpCnt = 0;

            yVelocity = 0;
        }


        if (jumpCnt < maxJumpCnt)
        {

            if (Input.GetButtonDown("Jump"))
            {
                yVelocity = jumpPower;

                jumpCnt++;

            }

        }


        yVelocity += gravity * Time.deltaTime;

        dir.y = yVelocity;

        cc.Move(dir * speed * Time.deltaTime);
    }

    
}
