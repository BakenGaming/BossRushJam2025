using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler_TopDown : MonoBehaviour, IAttackHandler
{
    #region Variables
    [SerializeField] private List<GameObject> weaponPoints;
    [SerializeField] private Transform firePoint;
    private IHandler _playerHandler;
    private IInputHandler _inputHandler;
    private bool initialized = false;

    #endregion

    #region Initialize
    public void Initialize()
    {
        int i = 0;
        _playerHandler = GetComponent<IHandler>();
        _inputHandler = GetComponent<IInputHandler>();
        foreach(GameObject _weaponPoint in weaponPoints)
        {
            Debug.Log($"Initialize Slot {i}");
            i++;
            _weaponPoint.GetComponent<IWeaponSlotHandler>().Initialize();
        }
        WeaponSystem.i.OnEquipNewWeapon += AddWeapon;
        initialized = true;
    }

    private void OnDisable() 
    {
        
    }

    #endregion

    #region Loop
    private void Update() 
    {
        if(GameManager.i.GetIsPaused()) return;
        else RotateWeapons();    
    }

    private void RotateWeapons()
    {
        if(!initialized) return;
        foreach(GameObject weapon in weaponPoints)
        {
            weapon.transform.Rotate(new Vector3(0, 0, _playerHandler.GetStatSystem().GetSpinRate() 
            + GameManager.i.GetBonusStats().GetBonusSpinRate()) * Time.deltaTime);
        }
    }
    #endregion

    #region Weapons
    public void AddWeapon(object sender, WeaponSystem.OnEquipNewWeaponEventArgs e)
    {
        IWeaponSlotHandler _handler = weaponPoints[e.slot].GetComponent<IWeaponSlotHandler>();
        if(_handler.IsSlotOccupied())
        {
            _handler.RemoveWeapon();
        }
        _handler.AddWeapon(e.newWeapon);

    }
    public Vector3 GetShootDirection() { return _inputHandler.GetShootDirection();}
    public Transform GetFirePoint(){ return firePoint;}
    

    #endregion
}
