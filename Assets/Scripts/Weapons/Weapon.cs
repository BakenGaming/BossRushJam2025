using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public static event Action triggerOffCooldown;
    [SerializeField] private WeaponStatsSO _weaponStatsSO;
    private bool onCooldown=false;
    private float coolDownTimer, timer = 1f;
    private void OnTriggerEnter2D(Collider2D _trigger) 
    {
        IAttackHandler _handler = _trigger.GetComponentInParent<IAttackHandler>();
        if(_weaponStatsSO != null && !onCooldown)
        {
            onCooldown = true;
            coolDownTimer = timer;
            GameObject newBullet = Instantiate(_weaponStatsSO.projectilePrefab, _handler.GetFirePoint().position, Quaternion.identity);
            newBullet.GetComponent<Projectile>().Initialize(_weaponStatsSO, _handler.GetShootDirection());
        }
    }

    private void Update() 
    {
        if(onCooldown) coolDownTimer -= Time.deltaTime;

        if(coolDownTimer <= 0) 
        {
            coolDownTimer = timer;
            onCooldown = false;
        }
    }
}
