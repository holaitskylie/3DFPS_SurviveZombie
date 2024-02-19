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
   
    [Header("Gun Settings")]
    [SerializeField] private GameObject player;
    [SerializeField] private WeaponSwitcher weaponSwitcher;
    [SerializeField] public bool shootToggle = false;
    [SerializeField] private GameObject[] gunImages;
    [SerializeField] private GameObject[] bulletImages;
    [SerializeField] private Text ammoAmount;
    
    void Start()
    {
        if(player == null)
            player = GameObject.Find("Player");        

        SetImages();
        SetAmmoText();
    }
       
    void Update()
    {
        SetAmmoText();
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
