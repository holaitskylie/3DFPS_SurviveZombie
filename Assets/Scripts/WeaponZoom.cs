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
    [SerializeField] private Animator animator;

    private bool zoomToggle = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {          

            if (zoomToggle == false)
            {
                zoomToggle = true;
                fpsCam.m_Lens.FieldOfView = zoomIn;

                animator.SetTrigger("ZoomIn");               
                
            }
            else
            {
                zoomToggle = false;
                fpsCam.m_Lens.FieldOfView = zoomOut;

                animator.SetTrigger("ZoomOut");
            }

        }
        
    }

}
