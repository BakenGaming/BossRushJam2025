using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler_TopDown : MonoBehaviour, IAttackHandler
{
    #region Variables
    [SerializeField] private List<GameObject> weaponPoints;
    [SerializeField] private Transform firePoint;
    private IInputHandler _handler;
    private float rotationSpeed=60f;
    private bool initialized = false;

    #endregion

    #region Initialize
    public void Initialize()
    {
        _handler = GetComponent<IInputHandler>();
        initialized = true;
        
    }

    private void OnDisable() 
    {
        
    }

    #endregion

    #region Loop
    private void Update() 
    {
        RotateWeapons();    
    }

    private void RotateWeapons()
    {
        if(!initialized) return;
        foreach(GameObject weapon in weaponPoints)
        {
            weapon.transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);
        }
    }
    #endregion

    #region Weapons
    public Vector3 GetShootDirection() { return _handler.GetShootDirection();}
    public Transform GetFirePoint(){ return firePoint;}

    #endregion
}
