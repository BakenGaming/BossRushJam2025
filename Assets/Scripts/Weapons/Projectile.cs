using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;
using System.Runtime.InteropServices;

public class Projectile : MonoBehaviour
{
    public static event Action<int> OnProjectileMiss;
    private Vector3 shootDir;
    private WeaponStatsSO _weaponStatsSO;
    private SpriteRenderer _renderer;
    private float lifeTimer;
    private Classification _classification;
    private Transform _target;
    public void Initialize(WeaponStatsSO _weapon, Vector3 _shootDir)
    {
        _renderer = GetComponent<SpriteRenderer>();
        
        if(_weapon.projectileSprite.Length > 1)
            _renderer.sprite = _weapon.projectileSprite[UnityEngine.Random.Range(0, _weapon.projectileSprite.Length)];
        
        shootDir = _shootDir;
        _weaponStatsSO = _weapon;
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(shootDir) - 90f);
        lifeTimer = _weaponStatsSO.lifeTime;
        _classification = _weaponStatsSO.classification;
    }

    void Update()
    {
        if(GameManager.i.GetIsPaused()) return;
        if(_classification == Classification.seeking) 
            HandleProjectileClassifications();
        else
            transform.position += shootDir * _weaponStatsSO.projectileSpeed * Time.deltaTime;
        UpdateLifeTimer();
    }

    private void HandleProjectileClassifications()
    {
        _target = GameManager.i.GetBossGO().transform;
        if(_target == null || _target.gameObject.activeInHierarchy == false) { ObjectPooler.EnqueueObject(this, _weaponStatsSO.projectileName); return;}

        Vector3 moveDir = (_target.transform.position - transform.position).normalized;

        float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0, 0, angle - 90f);
        
        transform.position += moveDir * _weaponStatsSO.projectileSpeed * Time.deltaTime;
    }

    private void UpdateLifeTimer()
    {
        lifeTimer -=Time.deltaTime;
        if (lifeTimer <= 0) 
        {
            ObjectPooler.EnqueueObject(this, _weaponStatsSO.projectileName);
        }
    }

    private void OnTriggerEnter2D(Collider2D _trigger) 
    {
        IDamageable damageable= _trigger.gameObject.GetComponent<IDamageable>();
        if(damageable != null)
        {
            if(_weaponStatsSO.classification == Classification.dot)
                damageable.TakeDamage(_weaponStatsSO.projectileDamage, true, _weaponStatsSO.dotAmount);
            else
                damageable.TakeDamage(_weaponStatsSO.projectileDamage, false, 0);
            ObjectPooler.EnqueueObject(this, _weaponStatsSO.projectileName);
        }
        else
        {
            if(_weaponStatsSO.loseSugarOnMiss)
            {
                OnProjectileMiss?.Invoke(1);
                TextPopUp.Create(transform.position, "-1", true);
            }
            ObjectPooler.EnqueueObject(this, _weaponStatsSO.projectileName);
        }
    }
}
