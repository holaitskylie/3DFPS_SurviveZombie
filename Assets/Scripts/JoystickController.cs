using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class JoystickController : MonoBehaviour
{
    public Joystick joystick;

    [Header("Camera Settings")]
    [SerializeField] private CinemachineVirtualCamera fpsCam;
    [SerializeField] private GameObject player;
    [SerializeField] private float rotationHorizontal;
    [SerializeField] private float rotationVertical;
    [SerializeField] private float rotationSpeed = 35f;

    [Header("Gun Settings")]
    [SerializeField] private WeaponSwitcher weaponSwitcher;
    
    void Start()
    {
        if(player == null)
            player = GameObject.Find("Player");        
    }

    // Update is called once per frame
    void Update()
    {
        rotationVertical = joystick.Vertical;
        rotationHorizontal = joystick.Horizontal;
        
        //Point : Ä«¸Þ¶ó ¾Þ±Û
        //fpsCam.transform.Rotate(-rotationVertical * rotationSpeed * Time.deltaTime, rotationHorizontal * rotationSpeed * Time.deltaTime, 0);

        player.transform.Rotate(0, rotationHorizontal * rotationSpeed * Time.deltaTime, 0);
        fpsCam.transform.Rotate(-rotationVertical * rotationSpeed * Time.deltaTime, 0, 0);
        
    }

    public void ChangeGunIndex()
    {
        weaponSwitcher.ChangeGun();
        weaponSwitcher.SetWeaponActive();
    }
}
