using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    #region Events and Variables
    public static event Action triggerOffCooldown;
    private const float radius = 1f;
    [SerializeField] private WeaponStatsSO _weaponStatsSO;
    private bool onCooldown=false;
    private float coolDownTimer, timer = 1f, _burstTime = .1f;
    
    #endregion
    #region Fire Weapon Trigger
    private void OnTriggerEnter2D(Collider2D _trigger) 
    {
        IAttackHandler _handler = GetComponentInParent<IAttackHandler>();
        if(_weaponStatsSO != null && !onCooldown)
        {
            onCooldown = true;
            coolDownTimer = timer;
            if(_weaponStatsSO.classification == Classification.normal ||
                _weaponStatsSO.classification == Classification.seeking ||_weaponStatsSO.classification == Classification.dot)
            {
                Projectile newProjectile = ObjectPooler.DequeueObject<Projectile>(_weaponStatsSO.projectileName);
                newProjectile.transform.position = _handler.GetFirePoint().position;
                newProjectile.gameObject.SetActive(true);
                newProjectile.Initialize(_weaponStatsSO, _handler.GetShootDirection());

            }
            else HandleSpecialWeapon();
        }
    }
    #endregion
    #region Handle Special Weapons
    private void HandleSpecialWeapon()
    {
        switch(_weaponStatsSO.classification)
        {
            case Classification.burst:
                StartCoroutine("Burst");
                break;
            case Classification.spread:
                HandleSpread();
                break;
        }
    }

    IEnumerator Burst()
    {
        IAttackHandler _handler = GetComponentInParent<IAttackHandler>();
        for (int i = 0; i < _weaponStatsSO.numberOfProjectilesPerShot; i++)
        {
            Projectile newProjectile = ObjectPooler.DequeueObject<Projectile>(_weaponStatsSO.projectileName);
            newProjectile.transform.position = _handler.GetFirePoint().position;
            newProjectile.gameObject.SetActive(true);
            newProjectile.Initialize(_weaponStatsSO, _handler.GetShootDirection());
            yield return new WaitForSeconds(_burstTime);
        }
    }

    private void HandleSpread()
    {
        IAttackHandler _handler = GetComponentInParent<IAttackHandler>();
        float angleStep = 360f / _weaponStatsSO.numberOfProjectilesPerShot;
        float angle = 0f;

        for (int i = 0; i < _weaponStatsSO.numberOfProjectilesPerShot; i++)
        {
            //Direction Calculations
            float projectileDirXPosition = _handler.GetFirePoint().position.x + Mathf.Sin((angle * Mathf.PI) / 180f) * radius;
            float projectileDirYPosition = _handler.GetFirePoint().position.y + Mathf.Cos((angle * Mathf.PI) / 180f) * radius;

            Vector3 projectileVector = new Vector3(projectileDirXPosition, projectileDirYPosition, 0);
            Vector3 projectileMoveDirection = (projectileVector - _handler.GetFirePoint().position).normalized;

            Projectile newProjectile = ObjectPooler.DequeueObject<Projectile>(_weaponStatsSO.projectileName);
            newProjectile.transform.position = _handler.GetFirePoint().position;
            newProjectile.gameObject.SetActive(true);
            newProjectile.Initialize(_weaponStatsSO, projectileMoveDirection);
            
            angle += angleStep;
        }
    }
    #endregion
    #region Loop
    private void Update() 
    {
        if(onCooldown) coolDownTimer -= Time.deltaTime;

        if(coolDownTimer <= 0) 
        {
            coolDownTimer = timer;
            onCooldown = false;
        }
    }
    #endregion
    #region Get Functions
    public WeaponStatsSO GetWeaponStats(){return _weaponStatsSO;}
    #endregion
}
