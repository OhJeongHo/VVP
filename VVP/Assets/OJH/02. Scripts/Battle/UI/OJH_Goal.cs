using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OJH_Goal : MonoBehaviour
{
    public GameObject trainHead;
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
        if (other.gameObject == trainHead)
        {
            print("∞Ò¿Œ");
            GameManager.instance.vrlose = true;
        }
    }
}
