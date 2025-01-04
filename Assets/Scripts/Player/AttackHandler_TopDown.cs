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
    private bool initialized = false, addingWeapon = false;
    private float _waitTime = .1f, _waitTimer;

    #endregion

    #region Initialize
    public void Initialize()
    {
        _playerHandler = GetComponent<IHandler>();
        _inputHandler = GetComponent<IInputHandler>();
        foreach(GameObject _weaponPoint in weaponPoints)
        {
            _weaponPoint.GetComponent<IWeaponSlotHandler>().Initialize();
        }
        WeaponSystem.i.OnEquipNewWeapon += AddWeapon;
        initialized = true;
    }

    private void OnDisable() 
    {
        WeaponSystem.i.OnEquipNewWeapon -= AddWeapon;
    }

    #endregion

    #region Loop
    private void Update() 
    {
        if(GameManager.i.GetIsPaused()) return;
        else RotateWeapons();  

        if(addingWeapon) _waitTimer -= Time.deltaTime;

        if(_waitTimer <=0) addingWeapon = false;  
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
        if(addingWeapon) return;
        _waitTimer = _waitTime;
        addingWeapon = true;
        IWeaponSlotHandler _handler = weaponPoints[e.slot].GetComponent<IWeaponSlotHandler>();
        if(_handler.IsSlotOccupied())
        {
            Debug.Log("Occupied");
            _handler.RemoveWeapon();
            _handler.AddWeapon(e.newWeapon);
        }
        else {Debug.Log("UnOccupied"); _handler.AddWeapon(e.newWeapon);}

    }
    public Vector3 GetShootDirection() { return _inputHandler.GetShootDirection();}
    public Transform GetFirePoint(){ return firePoint;}
    

    #endregion
}
