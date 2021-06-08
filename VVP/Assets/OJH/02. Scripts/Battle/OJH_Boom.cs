using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OJH_Boom : MonoBehaviour
{
    // public Transform explo;
    // public Rigidbody[] rbs;
    // Start is called before the first frame update
    Collider[] colls;
    void Start()
    {
        // rb.AddExplosionForce(10f, transform.position, 0.5f);
        colls = Physics.OverlapSphere(transform.position, 2f);
    }


    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isVR) return;
        // colls = Physics.OverlapSphere(transform.position, 2f);
        foreach (Collider coll in colls)
        {
            if (coll.gameObject.layer == 11)
            {
                coll.GetComponent<CharacterController>().enabled = false;
                coll.GetComponent<Rigidbody>().isKinematic = false;
                coll.attachedRigidbody.AddExplosionForce(1000f, transform.position, 2f, 1f);
                // ���� �κ�
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
            
            //if (coll.GetComponent<CharacterController>() != null)
            //{
            //    print("cc�� ����!");
            //    // �ش� ������Ʈ�� cc�� ����, �����ٵ��� isKinematic�� ���ش�
            //    coll.GetComponent<CharacterController>().enabled = false;
            //    coll.GetComponent<Rigidbody>().isKinematic = false;
            //}
            //if (coll.attachedRigidbody != null)
            //{
            //    print("�����ٵ� ����!");
            //    // ���߽�Ŵ
            //    coll.attachedRigidbody.AddExplosionForce(1000f, transform.position, 2f, 1f);
            //    if (coll.GetComponent<OJH_BattlePlayer>() != null)
            //    {
            //        if (coll.GetComponent<OJH_BattlePlayer>().sternMode == false)
            //        {
            //            coll.GetComponent<OJH_BattlePlayer>().sternMode = true;
            //            coll.GetComponent<OJH_BattlePlayer>().SternReset2();
            //        }

            //    }
            //}

        }
        
    }
}
