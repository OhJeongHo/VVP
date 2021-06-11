using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OJH_Fuelinput : MonoBehaviour
{
    float currTime;

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
        //if (other.gameObject.layer == 11)
        //{
        //    if (other.GetComponent<OJH_BattlePlayer>().fuel > 1)
        //    {
        //        currTime += Time.deltaTime;
        //        if (currTime >= 2f)
        //        {
        //            currTime = 0;
        //            other.GetComponent<OJH_BattlePlayer>().fuel--;
        //        }
        //    }
        //}
    }
}
