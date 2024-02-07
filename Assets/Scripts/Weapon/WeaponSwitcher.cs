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
        if (GameManager.instance.isDialogueActive || GameManager.instance.isGameOver)
            return;

        int previousWeapon = currentWeapon;
        
        //Ű���� Ű �Է¿� ���� currentWeapon �� ����(���� ��ü)
        //ProcessKeyInput();        

        //currentWeapon ���� ����Ǹ� ���� �޼��带 ȣ���Ͽ� ���ο� ���� Ȱ��ȭ
        if (previousWeapon != currentWeapon)
            SetWeaponActive();
    }

    public void ChangeGun()
    {
        //Change ��ư�� ������ ���� �� ��ü
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

}
