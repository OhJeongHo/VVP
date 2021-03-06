using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRUIRay : MonoBehaviour
{
    public static VRUIRay instance;
    // 오른손
    public Transform rightHand;
    // 이미지
    public Transform dot;
    public GameObject keyboard1, keyboard2, keyboard3;

    public InputField inputtext;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isVR == false)
        {
            return;
        }
        Ray ray = new Ray(rightHand.position, rightHand.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("UI"))
            {
                dot.gameObject.SetActive(true);
                dot.position = hit.point;
            }
            else
            {
                dot.gameObject.SetActive(false);
            }

            // 만약 점이 활성화 상태면
            if (dot.gameObject.activeSelf)
            {
                if (OVRInput.GetDown(OVRInput.Button.Any))
                {
                    // 버튼 스크립트를 가져온다
                    Button btn = hit.transform.GetComponent<Button>();
                    // 만약 btn이 null이 아니라면
                    if (btn != null)
                    {
                        btn.onClick.Invoke();
                    }
                    // 인풋필드를 가져온다.
                    inputtext = hit.transform.GetComponent<InputField>();
                    if (inputtext != null)
                    {   
                        keyboard1.SetActive(true);
                        keyboard2.SetActive(true);
                        keyboard3.SetActive(true);
                    }

                }
            }
        }
        else
        {
            dot.gameObject.SetActive(false);
        }
    }

    public void SetText(string t)
    {
        if(inputtext != null)
        {
            inputtext.text = t;
        }
    }
}
