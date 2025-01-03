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
        _renderer.sprite = _weapon.weaponSprite;
        shootDir = _shootDir;
        _weaponStatsSO = _weapon;
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(shootDir) - 90f);
        lifeTimer = _weaponStatsSO.lifeTime;
    }

    void Update()
    {
        transform.position += shootDir * _weaponStatsSO.projectileSpeed * Time.deltaTime;
        UpdateLifeTimer();
    }

    private void UpdateLifeTimer()
    {
        lifeTimer -=Time.deltaTime;
        if (lifeTimer <= 0) ObjectPooler.EnqueueObject(this, "Sprinkle");
        
    }
}
