using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;
using UnityEngine.InputSystem.Processors;

public class Projectile : MonoBehaviour
{
    public static event Action<int> OnProjectileMiss;
    [SerializeField] private Transform target;
    private Vector3 shootDir, bombSizeTriggerDistance;
    private Vector2 scaleVectorUp = new Vector2(3f, 3f);
    private Vector2 scaleVectorDown = new Vector2(.25f,.25f);
    private WeaponStatsSO _weaponStatsSO;
    private EnemyStatsSO _enemyStatsSO;
    private SpriteRenderer _renderer;
    private float lifeTimer, projectileSpeed;
    private Classification _classification;
    private Transform _target;
    private bool _isEnemyProjectile, _projectileHasLifetime;
    private string _name;
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
        _isEnemyProjectile = false;
    }

    public void InitializeNormalShot(EnemyStatsSO _enemyStats, Vector3 _shootDir, string _n)
    {
        _enemyStatsSO = _enemyStats;
        _renderer = GetComponent<SpriteRenderer>();
        _name = _n;
        shootDir = (_shootDir - transform.position).normalized;
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(shootDir) - 90f);
        lifeTimer = _enemyStatsSO.possibleProjectiles[1].lifeTime;
        projectileSpeed = _enemyStatsSO.possibleProjectiles[1].projectileSpeed;
        _projectileHasLifetime = true;
        _isEnemyProjectile = true;
    }

    public void InitializeBomb(EnemyStatsSO _enemyStats, Vector3 _target, string _n)
    {
        _renderer = GetComponent<SpriteRenderer>();
        _name = _n;
        _enemyStatsSO = _enemyStats;

        target.GetComponent<SpriteRenderer>().enabled = true;
        target.SetParent(null);
        target.position = _target;
        shootDir = (target.position - transform.position).normalized;
        bombSizeTriggerDistance = (target.position + transform.position) / 2f;
        lifeTimer = _enemyStatsSO.possibleProjectiles[0].lifeTime;
        projectileSpeed = _enemyStatsSO.possibleProjectiles[0].projectileSpeed;
 
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(shootDir) - 90f);

        _projectileHasLifetime = false;
        _isEnemyProjectile = true;
        TriggerGrow();
    }

    void Update()
    {
        if(_isEnemyProjectile) EnemyUpdate();
        else PlayerUpdate();

    }

    private void EnemyUpdate()
    {
        if(GameManager.i.GetIsPaused()) return;

        transform.position += shootDir * projectileSpeed * Time.deltaTime;
        if(!_projectileHasLifetime)
        {
            Vector2 newDistance = (target.position + transform.position) /2;
            if(Vector2.Distance(bombSizeTriggerDistance, newDistance) <= .5f)
            {
                TriggerShrink();
            }
            if(Vector2.Distance(target.position, transform.position) <= .5f)
            {
                Explode();
            }
        }
        else UpdateLifeTimer();
    }

    private void Explode()
    {
        target.GetComponent<SpriteRenderer>().enabled = false;
        GameObject newSplash = Instantiate(GameAssets.i.pfBerrySplash, transform.position, Quaternion.Euler( 0, 0, UnityEngine.Random.Range( 0, 4 ) * 90 ));
        newSplash.transform.SetParent(null);
        newSplash.GetComponent<Splash>().Initialize();        
        ObjectPooler.EnqueueObject(this, _name);
    }

    private void PlayerUpdate()
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

    private void TriggerGrow()
    {
        transform.localScale = Vector2.Lerp(transform.localScale, scaleVectorUp, 300  * Time.deltaTime);
    }

    private void TriggerShrink()
    {
        transform.localScale = Vector2.Lerp(transform.localScale, scaleVectorDown, 3  * Time.deltaTime);
    }

    private void UpdateLifeTimer()
    {
        lifeTimer -=Time.deltaTime;
        if(!_isEnemyProjectile)
        {
            if (lifeTimer <= 0) 
            {
                ObjectPooler.EnqueueObject(this, _weaponStatsSO.projectileName);
            }
        }
        else
        {
            if (lifeTimer <= 0 && _projectileHasLifetime) 
            {
                ObjectPooler.EnqueueObject(this, _name);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D _trigger) 
    {
        IDamageable damageable= _trigger.gameObject.GetComponent<IDamageable>();
        if(!_isEnemyProjectile)
        {
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
                if(_weaponStatsSO.loseSugarOnMiss && _trigger.tag != "Enemy Projectile")
                {
                    OnProjectileMiss?.Invoke(1);
                    TextPopUp.Create(transform.position, "-1", true);
                }
                ObjectPooler.EnqueueObject(this, _weaponStatsSO.projectileName);
            }
        }
        else
        {
            if(!_projectileHasLifetime) Explode();
            else ObjectPooler.EnqueueObject(this, _name);
        }
    }
}
