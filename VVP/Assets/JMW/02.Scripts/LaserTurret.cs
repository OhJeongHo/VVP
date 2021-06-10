using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LaserTurret : MonoBehaviourPun, IPunObservable
{
    private Quaternion currRot;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.gameObject.transform.rotation);
        }

        else
        {
            currRot = (Quaternion)stream.ReceiveNext();
            transform.rotation = Quaternion.Lerp(transform.rotation, currRot, 0.2f);
        }
        //public LobbyActiveJMW Lb;
    }

    //PhotonView pc = GameObject.Find("Turet01").GetComponentInChildren<LaserTurret>();

    GameObject par;

    public Transform Tankpos;
    public LsSwitch lss;

    //Renderer BColor;

    GameObject tempObj;

    bool tankCtrl = true;
    public GameObject pcplayer;

    GameObject cam;

    public Transform FirePoss;

    public GameObject bulletFactory;

    public LayerMask layerMask;

    public GameObject DangerMarker;

    public float rotSpeed = 200;

    float rotX = 0;
    float rotY = 0;


    public bool useVertical = false;
    public bool useHorizontal = false;

    



    void Start()
    {

    }

    
    void Update()
    {   
        

        TurretMove();

        if (Input.GetMouseButtonDown(0))
        {
            DangerMarkerShoot();
            tankCtrl = false;
            GameObject obj1 = GameObject.Find("line");
            obj1.SetActive(false);

            par = GameObject.Find("Weapon").transform.GetChild(1).gameObject;
            par.SetActive(true);
            SoundManager.instance.LaserChagerr();

            Invoke("ShootLaser", 3f);
            

            obj1.SetActive(true);
        }
    }

    public void TurretMove()
    {
        if (tankCtrl == true)
        {
            //    print("출력");
            //Camera cam = GetComponent<Camera>();
            //cam.enabled = false;

            //    //Lb.ff();


            float mx = Input.GetAxis("Mouse X");
            float my = Input.GetAxis("Mouse Y");

            if (useVertical == true)
            {
                rotX += -my * rotSpeed * Time.deltaTime;
            }

            if (useHorizontal == true)
            {
                rotY += mx * rotSpeed * Time.deltaTime;
            }


            rotX = Mathf.Clamp(rotX, -90, 90);

            transform.localEulerAngles = new Vector3(rotX, rotY, 0);



        }
    }

    void DangerMarkerShoot()
    {
        Vector3 NewPosition = new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z);
        Physics.Raycast(NewPosition, transform.forward, out RaycastHit hit, 1000f, layerMask);


        if (hit.transform.CompareTag("Wall"))
        {
            GameObject DangerMarkerClone = Instantiate(DangerMarker, NewPosition, transform.rotation);
            DangerMarkerClone.GetComponent<DangerLine>().EndPosition = hit.point;
        }
    }



   
    void ShootLaser()
    {
        SoundManager.instance.LaserShott();
        GameObject bullet = Instantiate(bulletFactory);
        bullet.transform.SetParent(FirePoss.transform); //자식파이어포스에서 총알생성 개헤맨부분
        bullet.transform.position = FirePoss.position;
        bullet.transform.rotation = FirePoss.rotation;
        Destroy(bullet, 3f);
        tankCtrl = false;

        pcplayer.transform.position = Tankpos.position;
        pcplayer.SetActive(true);
        //lss.GetComponent<LsSwitch>().BColor();

        GameObject.Find("LaserSW").GetComponent<LsSwitch>().Invoke("BColor",15f);


        gameObject.GetComponent<LaserTurret>().enabled = false;
        gameObject.GetComponent<Camera>().enabled = false;

        par.SetActive(false);
        tempObj = GameObject.Find("Camera").transform.GetChild(2).gameObject;
        tempObj.SetActive(false);
        




        //if (pcplayer.activeSelf == false)
        //{
        //    print("활성화");
        //    pcplayer.SetActive(false);

        //    pcplayer.SetActive(true);
        //}
        //this.enabled = false;






    }
    public void TankCt()
    {
        tankCtrl = true;
    }
}

    //public void ee()
    //{
    //    tankCtrl = false;
    //}

    
