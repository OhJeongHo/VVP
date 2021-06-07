using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class OJH_SelctSceneVR : MonoBehaviour
{
    LineRenderer lr;
    // public GameObject vrButton;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        DrawGuideLine();
    }
    void DrawGuideLine()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, hit.point);

            if (hit.transform.name.Contains("VR"))
            {
                if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
                {
                    Button btn = hit.transform.GetComponent<Button>();
                    btn.onClick.Invoke();
                }
            }
        }
        else
        {
            // 부딪힌 지점 없으면 오른손에서 몇미터 앞 까지만 라인 그려라
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, transform.position + transform.forward * 1);
        }
    }
}
