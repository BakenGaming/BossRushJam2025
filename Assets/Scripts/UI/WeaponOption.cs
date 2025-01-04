using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WeaponOption : MonoBehaviour
{
    public static event Action<GameObject> OnWeaponSelected;
    private GameObject weapon;

    public void Initialize(GameObject _weapon) 
    {
        weapon = _weapon;
        gameObject.GetComponent<Image>().sprite = _weapon.GetComponent<Weapon>().GetWeaponStats().weaponSprite;
    }
    private void Disable() 
    {
    }
    public void SelectWeapon()
    {
        OnWeaponSelected?.Invoke(weapon);
    }
}
