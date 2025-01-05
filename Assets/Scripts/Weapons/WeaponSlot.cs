using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoBehaviour, IWeaponSlotHandler
{
    private Transform thisSlotTransform;
    private bool slotOccupied;
    private GameObject equippedWeapon;
    public void AddWeapon(GameObject _weapon)
    {
        slotOccupied = true;
        equippedWeapon = Instantiate(_weapon, thisSlotTransform);
        Debug.Log($"{_weapon.GetComponent<Weapon>().GetWeaponStats().weaponName} Added");
    }


    public void RemoveWeapon()
    {
        slotOccupied = false;
        Debug.Log($"{equippedWeapon.GetComponent<Weapon>().GetWeaponStats().weaponName} Removed");
        Destroy(equippedWeapon.gameObject);
    }
  
    public bool IsSlotOccupied() {return slotOccupied;}
    public void Initialize() 
    {
        slotOccupied = false;
        thisSlotTransform = transform.Find("Slot");    
    }
    public GameObject GetEquippedWeapon(){return equippedWeapon;}
}
