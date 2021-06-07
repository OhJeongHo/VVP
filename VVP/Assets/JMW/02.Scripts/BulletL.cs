using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletL : MonoBehaviour
{
    public int bulletSpeed = 7;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.position += transform.up * bulletSpeed * Time.deltaTime;
    }
}
