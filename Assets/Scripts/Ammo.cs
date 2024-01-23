using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private AmmoSlot[] ammoSlots;

    [System.Serializable]
    private class AmmoSlot
    {
        public AmmoType ammoType;
        public int ammoAmount;
    }

    public int GetCurrentAmmo(AmmoType ammoType)
    {
        //ammoSlot을 파악한 뒤 어떤 종류의 탄약이 들어 있는지 알아낸다
        //해당 탄약의 수를 반환한다
        return GetAmmoSlot(ammoType).ammoAmount;
    }

    public void IncreaseCurrentAmmo(AmmoType ammoType, int ammoAmount)
    {
        GetAmmoSlot(ammoType).ammoAmount += ammoAmount;
    }
       
    public void ReduceCurrentAmmo(AmmoType ammoType)
    {
        //ammoSlot을 파악한 뒤 어떤 종류의 탄약이 들어있는지 알아낸다
        //해당 탄약을 감소 시킨다
        GetAmmoSlot(ammoType).ammoAmount--;
    }

    private AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        foreach(AmmoSlot slot in ammoSlots)
        {
            if(slot.ammoType == ammoType)
                return slot;
        }

        return null;
    }
}
