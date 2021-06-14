using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OJH_Potan : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            GameManager.instance.potanClose = true;
            GameManager.instance.vrClose = true;
        }
    }
}
