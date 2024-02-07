using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class JoystickController : MonoBehaviour
{
    public Joystick joystick;

    [Header("Player Move")]
    [SerializeField] private CharacterController characterController;
    public float moveSpeed = 5f;

    [Header("Camera Settings")]
    [SerializeField] private FixedTouchField fixedTouchField;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private CinemachineVirtualCamera fpsCam;
    [SerializeField] private GameObject player;
    [SerializeField] private float rotationHorizontal;
    [SerializeField] private float rotationVertical;
    [SerializeField] private float rotationSpeed = 35f;
    [SerializeField] private float camSensivity = 40f;
    private Vector2 lockAxis;

    [Header("Gun Settings")]
    [SerializeField] private WeaponSwitcher weaponSwitcher;
    [SerializeField] public bool shootToggle = false;
    [SerializeField] private GameObject[] gunImages;
    [SerializeField] private GameObject[] bulletImages;
    [SerializeField] private Text ammoAmount;
    
    void Start()
    {
        if(player == null)
            player = GameObject.Find("Player");

        fixedTouchField = GetComponentInChildren<FixedTouchField>();

        SetImages();
        SetAmmoText();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 Move = transform.right * joystick.Horizontal + transform.forward * joystick.Vertical;

        //characterController.Move(Move * moveSpeed * Time.deltaTime);


        //Way1. 플레이어와 자식 오브젝트인 카메라의 회전값을 다르게 주는 경우
        //rotationVertical = joystick.Vertical;       
        //rotationHorizontal = joystick.Horizontal;        

        //fpsCam.transform.Rotate(-rotationVertical * rotationSpeed * Time.deltaTime, rotationHorizontal * rotationSpeed * Time.deltaTime, 0);

        //player.transform.Rotate(0, rotationHorizontal * rotationSpeed * Time.deltaTime, 0);
        //fpsCam.transform.Rotate(-rotationVertical * rotationSpeed * Time.deltaTime, 0, 0);       

        //Way2. 조이스틱 축 값을 누적하여 쿼터니언에 적용
        //rotationVertical += joystick.Vertical * rotationSpeed * Time.deltaTime;
        //rotationHorizontal += joystick.Horizontal * rotationSpeed * Time.deltaTime;

        //fpsCam.transform.localRotation = Quaternion.AngleAxis(rotationHorizontal, Vector3.up);
        //fpsCam.transform.localRotation *= Quaternion.AngleAxis(rotationVertical, Vector3.left);

        //rotationVertical = Mathf.Clamp(rotationVertical, -70, 70);

        //touch sceen으로 카메라 조절
        cameraManager.lockAxis = fixedTouchField.TouchDist;
        //CameraControll();

        SetAmmoText();

    }

    private void CameraControll()
    {
        rotationHorizontal = lockAxis.x * camSensivity * Time.deltaTime;
        rotationVertical = lockAxis.y * camSensivity * Time.deltaTime;
        rotationSpeed -= rotationVertical;
        rotationSpeed = Mathf.Clamp(rotationSpeed, -90f, 90f);

        fpsCam.transform.localRotation = Quaternion.Euler(rotationSpeed, 0, 0);
        player.transform.Rotate(Vector3.up * rotationHorizontal);
    }

    public void Shoot()
    {      
        shootToggle = true;               
    }

    public void ChangeGunIndex()
    {
        weaponSwitcher.ChangeGun();
        weaponSwitcher.SetWeaponActive();
        SetImages();
        SetAmmoText();
    }

    private void SetImages()
    {        
        for(int i = 0; i < gunImages.Length; i++)
        {
            if (i == weaponSwitcher.currentWeapon)
            {
                gunImages[i].gameObject.SetActive(true);
                bulletImages[i].gameObject.SetActive(true);
            }
            else
            {
                gunImages[i].gameObject.SetActive(false);
                bulletImages[i].gameObject.SetActive(false);
            }
        }
    }

    private void SetAmmoText()
    {
        Ammo slot = player.GetComponent<Ammo>();

        if(weaponSwitcher.currentWeapon == 0)
            ammoAmount.text = slot.GetCurrentAmmo(AmmoType.Bullets).ToString();
        else if(weaponSwitcher.currentWeapon == 1)
            ammoAmount.text = slot.GetCurrentAmmo(AmmoType.Shells).ToString();
    }
}
