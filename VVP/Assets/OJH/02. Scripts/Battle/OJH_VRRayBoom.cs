using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OJH_VRRayBoom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Collider[] colls;

        colls = Physics.OverlapSphere(transform.position, 15f);
        foreach (Collider coll in colls)
        {
            if (coll.gameObject.layer == 11)
            {
                coll.GetComponent<CharacterController>().enabled = false;
                coll.GetComponent<Rigidbody>().isKinematic = false;
                Vector3 dir = transform.position - coll.transform.position;
                dir.Normalize();

                coll.GetComponent<Rigidbody>().AddExplosionForce(300f, transform.position, 10f, 3f);
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

        Destroy(gameObject, 1f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
