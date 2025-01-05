using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotUIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] UISlots;

    public void Initialize() 
    {
        WeaponSystem.i.OnEquipNewWeapon += AddWeapon;
        WeaponSystem.OnNewSlotSelected += IndicateSlot;

        foreach (GameObject _slot in UISlots)
        {
            _slot.GetComponent<Slot>().Initialize();
        }    
    }

    private void OnDisable() 
    {
        WeaponSystem.i.OnEquipNewWeapon -= AddWeapon;    
    }

    public void AddWeapon(object sender, WeaponSystem.OnEquipNewWeaponEventArgs e)
    {
        UISlots[e.slot].GetComponent<Slot>().Activate(e.newWeapon);
    }

    private void IndicateSlot(int _slot)
    {
        UISlots[_slot].GetComponent<Slot>().ActivateIndicator();
    }
}
