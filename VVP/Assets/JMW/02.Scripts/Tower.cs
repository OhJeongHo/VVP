using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {


    //발판

    public Vector3 eulerAngles;

    public bool Catcher = false;
	public Transform shootElement;
    public GameObject Towerbug;
    public Transform LookAtObj;    
    public GameObject bullet;
    public GameObject DestroyParticle;
    public Vector3 impactNormal_2;
    public Transform target;
    public int dmg = 10;
    public float shootDelay;
    //public float DeactDelay;
    bool isShoot;
    public Animator anim_2;
    public TowerHP TowerHp;    
    private float homeY;
    public Transform fs;

    public float x;
    public float y;
    public float z;

    // for Catcher tower 

    void Start()
    {
        anim_2 = GetComponent<Animator>();
        homeY = LookAtObj.transform.localRotation.eulerAngles.y;
        //TowerHp = Towerbug.GetComponent<TowerHP>();

        target = GameObject.FindWithTag("VRPlayer").transform;
    }
           

    
    // for Catcher tower attack animation

    void GetDamage()

    {
        if (target)
        {
            target.GetComponent<EnemyHp>().Dmg(dmg);
        }
    }




    void Update () {
       

        
        //// Tower`s rotate

        //if (target)
        //{   
        //    Vector3 dir = target.transform.position - LookAtObj.transform.position;
        //        dir.y = 0; 
        //        Quaternion rot = Quaternion.LookRotation(dir);                
        //        LookAtObj.transform.rotation = Quaternion.Slerp( LookAtObj.transform.rotation, rot, 5 * Time.deltaTime);
        //}
      
        //else
        //{
            
        //    Quaternion home = new Quaternion (0, homeY, 0, 1);
            
        //    LookAtObj.transform.rotation = Quaternion.Slerp(LookAtObj.transform.rotation, home, Time.deltaTime);                        
        //}


        // Shooting
               

            //if (!isShoot)
            //{
            //    StartCoroutine(shoot());

            //}

        
        //if (Catcher == true)
        //{
        //    if (!target || target.CompareTag("Dead"))
        //    {

        //        StopCatcherAttack();
        //    }

        //}

        // Destroy

        //if (TowerHp.CastleHp <= 0)        {
            
        //    Destroy(gameObject);
        //    DestroyParticle = Instantiate(DestroyParticle, Towerbug.transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal_2)) as GameObject;            
        //    Destroy(DestroyParticle, 3);
        //}
        if(deactRot)
        {
            Vector3 dirr = fs.transform.position - Towerbug.transform.position;
            Towerbug.transform.forward = Vector3.Lerp(Towerbug.transform.forward, dirr, 0.7f * Time.deltaTime);
        }

        if(moveRot)
        {
            Vector3 dir = target.transform.position - Towerbug.transform.position;
            
            //에너미 방향으로
            Towerbug.transform.forward = Vector3.Lerp(Towerbug.transform.forward, dir, 0.1f * Time.deltaTime);


            
            //Quaternion targetRot = Quaternion.LookRotation(Towerbug.transform.position - target.transform.position);
            //transform.rotation = Quaternion.Lerp(targetRot, Quaternion.EulerAngles(0, 0, 0), Time.deltaTime * 5);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            moveRot = false;
            deactRot = true;

            Invoke("OnDeacti", 1);
            
            Invoke("OnSetAnim", 1);


            //Invoke("OnSetAnim", 1);


            //Quaternion targetRot = Quaternion.EulerAngles(0, 0, 0);
            //x -= Time.deltaTime * 0.1f;
            //Quaternion targetRot = Quaternion.LookRotation(Towerbug.transform.position - target.transform.position);
            //Quaternion startRot = Quaternion.EulerAngles(0, 0, 0);
            //transform.rotation = Quaternion.Lerp(targetRot, startRot, Time.deltaTime * 5);


            //Vector3 Rot = Quaternion.Lerp(Towerbug.transform.rotation, Quaternion.EulerAngles(0, 0, 0), Time.deltaTime * 10f).eulerAngles;
            //fs.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            //Invoke("OnSetAnim", 1);
        }
        //    //Towerbug.transform.forward = Vector3.Lerp(Quaternion.EulerAngles(0, 0, 0), 0.1f * Time.deltaTime);
        //    //원래 방향으로
        //    //fs.transform.position = Vector3.Lerp(Towerbug.transform.forward, dir, 0.1f * Time.deltaTime);
        //}

        //if(deactRot)
        //{
        //    Vector3 dir = Towerbug.transform.position - target.transform.position;
        //    Towerbug.transform.forward = Vector3.Lerp(Towerbug.transform.forward, dir, 0.1f * Time.deltaTime);
        //}

    }

    bool moveRot;
    bool deactRot;
	IEnumerator shoot()
	{
        anim_2.SetBool("Act", true);

        //deactRot = false;
        moveRot = false;
        isShoot = true;
        yield return new WaitForSeconds(1.6f);
        moveRot = true;
        

        yield return new WaitForSeconds(shootDelay - 1f);
        moveRot = false;

        
        deactRot = true;

        Invoke("OnDeacti", 1);

        Invoke("OnSetAnim", 1);

        //deactRot = true;

        //Invoke("OnDeacti", 1);

        //Invoke("OnSetAnim", 1);

        //deactRot = true;

        //yield return new WaitForSeconds(DeactDelay - 1.6f);

        if (target && Catcher == false)
        {
            GameObject b = GameObject.Instantiate(bullet, shootElement.position, Quaternion.identity) as GameObject;
            b.GetComponent<TowerBullet>().target = target.transform;
            b.GetComponent<TowerBullet>().twr = this;
          
        }
        isShoot = false;
	}



        void StopCatcherAttack()

        {                
            target = null;
            //anim_2.SetBool("Attack", false);
                   
        }


    

    public void shoots()
    {
        if (!isShoot)
        {
            
            StartCoroutine(shoot());
        }
    }

    public void OnSetAnim()
    {
        anim_2.SetTrigger("Reset");
    }    

    public void OnDeacti()
    {
        deactRot = false;
        Towerbug.transform.rotation = Quaternion.EulerAngles(0, 0, 0);
        anim_2.SetBool("Act", false);
    }
    

}



