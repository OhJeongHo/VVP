using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniController : MonoBehaviour
{
    [SerializeField]

    private Animator animator;

    CharacterController cc;

    float gravity = -9.8f;

    float jumpPower = 3;

    float yVelocity;

    int jumpCnt = 0;

    int maxJumpCnt = 1;

    public float speed = 5;

    Animator anim;




    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");


        Vector3 moveVector = new Vector3(h, 0f, v);
        animator.SetBool("isMove", moveVector.magnitude > 0);


        //점프애니메이션
        if(Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("isJump");
        }

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

            //if (Input.GetButtonDown("Jump"))
            //{
            //    yVelocity = jumpPower;

            //    jumpCnt++;

            //}

        }


        yVelocity += gravity * Time.deltaTime;

        dir.y = yVelocity;

        cc.Move(dir * speed * Time.deltaTime);
        
    }
}
