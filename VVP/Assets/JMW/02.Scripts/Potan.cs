using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potan : MonoBehaviour
{
    public float Speed;
    public Transform target;
    void Start()
    {

    }


    void Update()
    {
        if (target)
        {

            //transform.LookAt(target);
            transform.position = Vector3.forward * Time.deltaTime * Speed;
        }

        if (transform.position == target.position)
        {
            hit();
        }
    }

    void hit()
    {
        Destroy(gameObject);
    }

}