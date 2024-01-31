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
        //Way1. �÷��̾�� �ڽ� ������Ʈ�� ī�޶��� ȸ������ �ٸ��� �ִ� ���
        //rotationVertical = joystick.Vertical;       
        //rotationHorizontal = joystick.Horizontal;        
        
        //fpsCam.transform.Rotate(-rotationVertical * rotationSpeed * Time.deltaTime, rotationHorizontal * rotationSpeed * Time.deltaTime, 0);

        //player.transform.Rotate(0, rotationHorizontal * rotationSpeed * Time.deltaTime, 0);
        //fpsCam.transform.Rotate(-rotationVertical * rotationSpeed * Time.deltaTime, 0, 0);       

        //Way2. ���̽�ƽ �� ���� �����Ͽ� ���ʹϾ� ����
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
