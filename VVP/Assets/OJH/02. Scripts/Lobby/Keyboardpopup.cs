using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keyboardpopup : MonoBehaviour
{
    public InputField inputtext;

    public GameObject keyboard1, keyboard2, keyboard3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // inputtext.text = 
    }

    public void SetText(string text)
    {
        VRUIRay.instance.SetText(text);
      
    }
    public void InputEnd()
    {
        keyboard2.SetActive(false);
        keyboard3.SetActive(false);
        keyboard1.SetActive(false);
    }
}
