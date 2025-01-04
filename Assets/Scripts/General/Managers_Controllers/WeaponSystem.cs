using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    private static WeaponSystem _i;
    public static WeaponSystem i { get { return _i; } }

    public event EventHandler<OnEquipNewWeaponEventArgs> OnEquipNewWeapon;
    public class OnEquipNewWeaponEventArgs : EventArgs
    {
        public GameObject newWeapon;
        public int slot;
    }

    [SerializeField] private GameObject[] gameWeapons;

    private void Awake() 
    {
        _i = this;    
    }
    private void Update() 
    {
#if UNITY_EDITOR
    if(Input.GetKeyDown(KeyCode.J))
    {
        OnEquipNewWeapon?.Invoke(this, 
            new OnEquipNewWeaponEventArgs { newWeapon = gameWeapons[UnityEngine.Random.Range(0, gameWeapons.Length)],
            slot = UnityEngine.Random.Range(0,8) }
            );
    }

#endif        
    }

    public GameObject[] GetWeapons(){return gameWeapons;}
}