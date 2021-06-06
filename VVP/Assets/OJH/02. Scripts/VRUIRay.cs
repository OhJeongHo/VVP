using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRUIRay : MonoBehaviour
{
    public static VRUIRay instance;
    // ������
    public Transform rightHand;
    // �̹���
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

            // ���� ���� Ȱ��ȭ ���¸�
            if (dot.gameObject.activeSelf)
            {
                if (OVRInput.GetDown(OVRInput.Button.Any))
                {
                    // ��ư ��ũ��Ʈ�� �����´�
                    Button btn = hit.transform.GetComponent<Button>();
                    // ���� btn�� null�� �ƴ϶��
                    if (btn != null)
                    {
                        btn.onClick.Invoke();
                    }
                    // ��ǲ�ʵ带 �����´�.
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
