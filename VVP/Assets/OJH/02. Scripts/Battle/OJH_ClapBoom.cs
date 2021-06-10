using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OJH_ClapBoom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1f);

        Collider[] colls;

        colls = Physics.OverlapSphere(transform.position, 10f);
        foreach (Collider coll in colls)
        {
            if (coll.gameObject.layer == 11)
            {
                coll.GetComponent<CharacterController>().enabled = false;
                coll.GetComponent<Rigidbody>().isKinematic = false;
                Vector3 dir = transform.position - coll.transform.position;
                dir.Normalize();

                coll.GetComponent<Rigidbody>().AddExplosionForce(1000f, transform.position, 20f, 2f);
                // 수정 부분
                if (coll.GetComponent<OJH_BattlePlayer>().rocketMode == true)
                {
                    coll.GetComponent<OJH_BattlePlayer>().rocketMode = false;
                }
                if (coll.GetComponent<OJH_BattlePlayer>().sternMode == false)
                {
                    coll.GetComponent<OJH_BattlePlayer>().sternMode = true;
                    coll.GetComponent<OJH_BattlePlayer>().SternReset2();
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
