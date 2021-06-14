using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OJH_VrUI : MonoBehaviour
{
    public GameObject vrwin;
    public GameObject vrlose;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.vrwin)
        {
            vrwin.SetActive(true);
        }
        if (GameManager.instance.vrlose)
        {
            vrlose.SetActive(true);
        }
    }
}
