using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : MonoBehaviour
{

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



        if(Input.GetMouseButtonDown(0))
        {
            DangerMarkerShoot();
            GameObject obj1 = GameObject.Find("line");
            obj1.SetActive(false);
            Invoke("ShootLaser", 3f);
            
            obj1.SetActive(true);
        }
    }

    void DangerMarkerShoot()
    {
        Vector3 NewPosition = new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z);
        Physics.Raycast(NewPosition, transform.forward, out RaycastHit hit, 1000f, layerMask);


        if(hit.transform.CompareTag("Wall"))
        {
            GameObject DangerMarkerClone = Instantiate(DangerMarker, NewPosition, transform.rotation);
            DangerMarkerClone.GetComponent<DangerLine>().EndPosition = hit.point;
        }
    }

    void ShootLaser()
    {
        
        GameObject bullet = Instantiate(bulletFactory);
        bullet.transform.SetParent(FirePoss.transform); //자식파이어포스에서 총알생성 개헤맨부분
        bullet.transform.position = FirePoss.position;
        bullet.transform.rotation = FirePoss.rotation;
        Destroy(bullet, 3f);
        






    }
    
}
