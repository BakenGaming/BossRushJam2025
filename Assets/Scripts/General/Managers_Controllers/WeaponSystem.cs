using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    public static event Action<int> OnNewSlotSelected;
    private static WeaponSystem _i;
    public static WeaponSystem i { get { return _i; } }

    public event EventHandler<OnEquipNewWeaponEventArgs> OnEquipNewWeapon;
    public class OnEquipNewWeaponEventArgs : EventArgs
    {
        public GameObject newWeapon;
        public int slot;
    }

    [SerializeField] private GameObject[] gameWeapons;
    private int _newSlot;
    public void Initialize() 
    {
        _i = this;  
        WeaponOption.OnWeaponSelected += AddNewWeapon;
        Roulette.OnSpinWheelActive += SelectNewSlot;
    }
    private void OnDisable() 
    {
        WeaponOption.OnWeaponSelected -= AddNewWeapon;
        Roulette.OnSpinWheelActive -= SelectNewSlot;    
    }

    public void EquipFirstWeapon()
    {
        SelectNewSlot();
        AddNewWeapon(gameWeapons[UnityEngine.Random.Range(0, gameWeapons.Length)]);
    }
    public void AddNewWeapon(GameObject _weapon)
    {
        OnEquipNewWeapon?.Invoke(this, 
            new OnEquipNewWeaponEventArgs { newWeapon = _weapon,
            slot = _newSlot }
            );
    }

    private void SelectNewSlot()
    {
        _newSlot = UnityEngine.Random.Range(0,8);
        OnNewSlotSelected?.Invoke(_newSlot);
    }
    public GameObject[] GetWeapons(){return gameWeapons;}
}
