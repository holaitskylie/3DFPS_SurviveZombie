using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WeaponZoom : MonoBehaviour
{
    //[SerializeField] private Camera fpsCam;
    [SerializeField] private CinemachineVirtualCamera fpsCam;
    [SerializeField] private float zoomOut = 40f;
    [SerializeField] private float zoomIn = 20f;
    [SerializeField] private GameObject sniperGun;

    private bool zoomToggle = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Animator zoomAnim = sniperGun.GetComponent<Animator>();

            if (zoomToggle == false)
            {
                zoomToggle = true;
                fpsCam.m_Lens.FieldOfView = zoomIn;

                
                if (zoomAnim != null)
                {
                    zoomAnim.SetTrigger("ZoomIn");
                }
                
            }
            else
            {
                zoomToggle = false;
                fpsCam.m_Lens.FieldOfView = zoomOut;

                if(zoomAnim != null)
                {
                    zoomAnim.SetTrigger("ZoomOut");
                }
            }

        }
        
    }

}
