using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ringOut : MonoBehaviour
{
    public GameObject apr;

    

    //List<GameObject> objs = new List<GameObject>();

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {

        OJH_BattlePlayer pm = other.gameObject.GetComponent<OJH_BattlePlayer>();
        if(pm)
        {
            pm.enabled = false;
        }
        other.gameObject.transform.position = apr.transform.position;
        print("Ãæµ¹");
        ScoreCnt.vrCnt += 1;

        StartCoroutine(Delay(pm));
        }
    }

    IEnumerator Delay(OJH_BattlePlayer pm)
    {
        yield return new WaitForSeconds(0.1f);
        pm.enabled = true;
    }
  
}
