using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OJH_PCPlayer : MonoBehaviour
{
    enum PcPlayerState
    {
        Idle,
        Run,
        Jump,
        Fly,
        Stern
    }

    Vector3 dir;
    float yVelocity;
    public float jumpCnt;
    public float maxjumpCnt = 1;
    public float jumpPower = 2;
    float gravity = -9.8f;
    float currTime;

    PcPlayerState state;
    Animator anim;

    public GameObject myVrModel;
    public GameObject myPcModel;

    bool isVR;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
