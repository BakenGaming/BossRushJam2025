using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponSlotHandler
{
    public void Initialize();
    public bool IsSlotOccupied();
    public void AddWeapon(GameObject _weapon);
    public void RemoveWeapon();
}
