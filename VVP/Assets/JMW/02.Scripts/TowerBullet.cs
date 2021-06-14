using UnityEngine;
using System.Collections;

public class TowerBullet : MonoBehaviour {

    public GameObject smk;
    public float Speed;
    public Transform target;
    public GameObject impactParticle; // bullet impact
    Vector3 dir;
    
    public Vector3 impactNormal; 
    Vector3 lastBulletPosition; 
    public Tower twr; 


    //// destroy bullet when get to the target, instantiate hit FX
    //void hit()
    //{
    //    Destroy(gameObject);
    //    //impactParticle = Instantiate(impactParticle, target.transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;
    //    if (target)
    //    {
    //        //impactParticle.transform.parent = target.transform;
    //    }
    //    //Destroy(impactParticle, 3);
    //    return;
    //}

    private void Start()
    {
        dir = target.transform.position - transform.position;
        dir.Normalize();
        Destroy(gameObject, 10f);
    }


    void Update() {

        transform.position += dir * Speed * Time.deltaTime;
        Invoke("smokeOn", 0.5f);

        //// Bullet move

        //if (target)
        //{

        //    transform.LookAt(target);
        //    transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * Speed);
        //    Invoke("smokeOn", 0.5f);
        //    lastBulletPosition = target.transform.position;

        //    if (transform.position == target.position)
        //    {
        //        hit();
        //    }
        //}

        //// Move bullet ( enemy was disapeared )

        //else

        //{


        //    transform.position = Vector3.MoveTowards(transform.position, lastBulletPosition, Time.deltaTime * Speed);

        //    if (transform.position == lastBulletPosition)
        //    {

        //        hit();

        //    }
        //}     
    }

    // Bullet hit


    void smokeOn()
    {
        smk.SetActive(true);
    }

//    void OnTriggerEnter(Collider other) // tower`s hit if bullet reached the enemy
//        {
//            if (other.gameObject.transform == target)
//            {

//                if (target.CompareTag("enemyBug"))
//                {

//                    target.GetComponent<EnemyHp>().Dmg(twr.dmg);
//                hit();
//            }


//                if (other.gameObject.CompareTag("Tank"))
//                {
//                    target.GetComponent<TowerHP>().Dmg_2(twr.dmg);
//                hit();
//            }
//            }



        
//    }
 
}



