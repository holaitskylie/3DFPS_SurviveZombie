using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] public int currentWeapon = 0;

    private void Start()
    {        
        SetWeaponActive();
    }

    //현재 선택된 무기를 활성화하고 나머지 무기들은 비활성화
    public void SetWeaponActive()
    {
        int weaponIndex = 0;

        foreach(Transform weapon in transform)
        {
            if (weaponIndex == currentWeapon)
                weapon.gameObject.SetActive(true);            
            else
                weapon.gameObject.SetActive(false);

            weaponIndex++;
        }
    }

    private void Update()
    {
        int previousWeapon = currentWeapon;

        //TODO : 키보드  Key 말고 버튼으로 교체하도록 변경
        //키보드 키 입력에 따라 currentWeapon 값 갱신(무기 교체)
        ProcessKeyInput();

        //마우스 휠에 따라 currentWeapon 값 갱신(무기 교체)
        ProcessScrollWheel();
        

        //currentWeapon 값이 변경되면 다음 메서드를 호출하여 새로운 무기 활성화
        if (previousWeapon != currentWeapon)
            SetWeaponActive();
    }

    public void ChangeGun()
    {
        if (currentWeapon >= transform.childCount - 1)
            currentWeapon = 0;
        else
            currentWeapon++;

    }
        
    private void ProcessKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 0;
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 1;
        }

    }

    private void ProcessScrollWheel()
    {
        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (currentWeapon >= transform.childCount - 1)
                currentWeapon = 0;
            else
                currentWeapon++;
        }

        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (currentWeapon <= 0)
                currentWeapon = transform.childCount - 1;
            else
                currentWeapon--;
        }
       
    }

}
