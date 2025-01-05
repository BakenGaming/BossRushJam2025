using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WeaponOption : MonoBehaviour
{
    public static event Action<GameObject> OnWeaponSelected;
    private RectTransform _left, _right;
    private Vector3 staticMovementVector = new Vector3(-100, 0, 0);
    private GameObject weapon;

    public void Initialize(GameObject _weapon) 
    {
        weapon = _weapon;
        gameObject.GetComponent<Image>().sprite = _weapon.GetComponent<Weapon>().GetWeaponStats().weaponSprite;
    }
    private void Disable() 
    {
    }

    public void Move(float _spinRate, RectTransform _l, RectTransform _r)
    {
        _left = _l;
        _right = _r;
        GetComponent<RectTransform>().localPosition += staticMovementVector * (_spinRate * Time.deltaTime);
        if(GetComponent<RectTransform>().localPosition.x <= _left.localPosition.x)
        {
            GetComponent<RectTransform>().localPosition = new Vector3(_right.localPosition.x, 0, 0);
            Debug.Log($"{GetComponent<RectTransform>().localPosition.x} = {new Vector3(_right.position.x, 0, 0)} = {_right.localPosition}");
        }
    }
    public void SelectWeapon()
    {
        OnWeaponSelected?.Invoke(weapon);
    }
}
