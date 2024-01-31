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
    [SerializeField] public bool shootToggle = false;
    
    void Start()
    {
        if(player == null)
            player = GameObject.Find("Player");        
    }

    // Update is called once per frame
    void Update()
    {
        //Way1. 플레이어와 자식 오브젝트인 카메라의 회전값을 다르게 주는 경우
        //rotationVertical = joystick.Vertical;       
        //rotationHorizontal = joystick.Horizontal;        
        
        //fpsCam.transform.Rotate(-rotationVertical * rotationSpeed * Time.deltaTime, rotationHorizontal * rotationSpeed * Time.deltaTime, 0);

        //player.transform.Rotate(0, rotationHorizontal * rotationSpeed * Time.deltaTime, 0);
        //fpsCam.transform.Rotate(-rotationVertical * rotationSpeed * Time.deltaTime, 0, 0);       

        //Way2. 조이스틱 축 값을 누적하여 쿼터니언에 적용
        rotationVertical += joystick.Vertical * rotationSpeed * Time.deltaTime;
        rotationHorizontal += joystick.Horizontal * rotationSpeed * Time.deltaTime;
        
        fpsCam.transform.localRotation = Quaternion.AngleAxis(rotationHorizontal, Vector3.up);
        fpsCam.transform.localRotation *= Quaternion.AngleAxis(rotationVertical, Vector3.left);

        rotationVertical = Mathf.Clamp(rotationVertical, -70, 70);
        
    }

    public void Shoot()
    {
        Debug.Log("shoot toggle : " + shootToggle);

        shootToggle = true;
        Debug.Log("shoot toggle is now : " + shootToggle);        
    }

    public void ChangeGunIndex()
    {
        weaponSwitcher.ChangeGun();
        weaponSwitcher.SetWeaponActive();
    }
}
