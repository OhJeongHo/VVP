using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelInputing : MonoBehaviour
{
    public Text Fuel;
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
        if (other.gameObject.layer == 11)
        {
            if (other.GetComponent<OJH_BattlePlayer>().playerFuel.GetComponent<MeshRenderer>().enabled)
            {
                Fuel.gameObject.SetActive(true);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            if (other.GetComponent<OJH_BattlePlayer>().playerFuel.GetComponent<MeshRenderer>().enabled)
            {
                currTime += Time.deltaTime;
                Fuel.text = "���Ḧ �����ϰ� �ֽ��ϴ�.\n( " + currTime.ToString("N2") + "�� / 3 �� )";
            }
            if (other.GetComponent<OJH_BattlePlayer>().playerFuel.GetComponent<MeshRenderer>().enabled == false)
            {
                currTime = 0;
                Fuel.gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            if (other.GetComponent<OJH_BattlePlayer>().playerFuel.GetComponent<MeshRenderer>().enabled)
            {
                if (Fuel.gameObject.activeSelf)
                {
                    Fuel.gameObject.SetActive(false);
                }
                currTime = 0;
            }
        }
    }
}
