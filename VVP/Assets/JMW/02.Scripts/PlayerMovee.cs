using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovee : MonoBehaviour
{

    CharacterController cc;


    public float speed = 5;


    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);

        dir = Camera.main.transform.TransformDirection(dir);
        dir.Normalize();

        cc.Move(dir * speed * Time.deltaTime);
    }
}
