using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OJH_ModeUI : MonoBehaviour
{
    public GameObject mode1, mode2, mode3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickLeft()
    {
        if (mode1.activeSelf)
        {
            mode1.SetActive(false);
            mode3.SetActive(true);
            return;
        }
        if (mode2.activeSelf)
        {
            mode2.SetActive(false);
            mode1.SetActive(true);
            return;

        }
        if (mode3.activeSelf)
        {
            mode3.SetActive(false);
            mode2.SetActive(true);
            return;
        }
    }

    public void OnClickRight()
    {
        if (mode1.activeSelf)
        {
            mode1.SetActive(false);
            mode2.SetActive(true);
            return;
        }
        if (mode2.activeSelf)
        {
            mode2.SetActive(false);
            mode3.SetActive(true);
            return;
        }
        if (mode3.activeSelf)
        {
            mode3.SetActive(false);
            mode1.SetActive(true);
            return;
        }
    }
}
