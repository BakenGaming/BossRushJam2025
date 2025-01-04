using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Projectile : MonoBehaviour
{
    private Vector3 shootDir;
    private WeaponStatsSO _weaponStatsSO;
    private SpriteRenderer _renderer;
    private float lifeTimer;
    public void Initialize(WeaponStatsSO _weapon, Vector3 _shootDir)
    {
        _renderer = GetComponent<SpriteRenderer>();
        
        if(_weapon.projectileSprite.Length > 1)
            _renderer.sprite = _weapon.projectileSprite[Random.Range(0, _weapon.projectileSprite.Length)];
        
        shootDir = _shootDir;
        _weaponStatsSO = _weapon;
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(shootDir) - 90f);
        lifeTimer = _weaponStatsSO.lifeTime;
    }

    void Update()
    {
        if(GameManager.i.GetIsPaused()) return;

        transform.position += shootDir * _weaponStatsSO.projectileSpeed * Time.deltaTime;
        UpdateLifeTimer();
    }

    private void UpdateLifeTimer()
    {
        lifeTimer -=Time.deltaTime;
        if (lifeTimer <= 0) ObjectPooler.EnqueueObject(this, "Sprinkle");
    }

    private void OnTriggerEnter2D(Collider2D _trigger) 
    {
        IDamageable damageable= _trigger.gameObject.GetComponent<IDamageable>();
        if(damageable != null)
        {
            damageable.TakeDamage(_weaponStatsSO.projectileDamage);
            ObjectPooler.EnqueueObject(this, "Sprinkle");
        }
        else
        {
            ObjectPooler.EnqueueObject(this, "Sprinkle");
        }
    }
}
