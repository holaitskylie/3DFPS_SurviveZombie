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

    //���� ���õ� ���⸦ Ȱ��ȭ�ϰ� ������ ������� ��Ȱ��ȭ
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

        //TODO : Ű����  Key ���� ��ư���� ��ü�ϵ��� ����
        //Ű���� Ű �Է¿� ���� currentWeapon �� ����(���� ��ü)
        ProcessKeyInput();

        //���콺 �ٿ� ���� currentWeapon �� ����(���� ��ü)
        ProcessScrollWheel();
        

        //currentWeapon ���� ����Ǹ� ���� �޼��带 ȣ���Ͽ� ���ο� ���� Ȱ��ȭ
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
