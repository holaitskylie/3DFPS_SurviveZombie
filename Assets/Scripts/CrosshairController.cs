using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CrosshairController : MonoBehaviour
{
    [SerializeField] private Camera playerCam;
    [SerializeField] private float range = 100f;

    [SerializeField] private GameObject target;

    private Color originColor;
    [SerializeField] private Image crosshairImage;

    // Start is called before the first frame update
    void Start()
    {
        originColor = crosshairImage.color;         
    }

    // Update is called once per frame
    void Update()
    {
        SetColor();    
    }

    public GameObject GetTarget()
    {
        RaycastHit hit;

        Vector3 rayDir = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = playerCam.ScreenPointToRay(rayDir);

        if(Physics.Raycast(ray, out hit, range))
        {
            target = hit.transform.gameObject;                      

            return target;
        }
        else
        {
            target = null;
            return null;
        }
    }

    public void SetColor()
    {      
        if (GetTarget() != null)
        {
            if(GetTarget().gameObject.GetComponent<IDamageable>()!= null)
            {
                Debug.Log("CAN SHOOTABLE");
                crosshairImage.color = new Color32(255, 155, 0, 255);
            }           
            else
            {
                crosshairImage.color = originColor;
            }
           
        }
    }
}
