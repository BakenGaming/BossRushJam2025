using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Projectile : MonoBehaviour
{
    private Vector3 shootDir;
    private WeaponStatsSO _weaponStatsSO;
    public void Initialize(WeaponStatsSO _weapon, Vector3 _shootDir)
    {
        shootDir = _shootDir;
        _weaponStatsSO = _weapon;
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(shootDir) - 90f);
    }

    void Update()
    {
        transform.position += shootDir * _weaponStatsSO.projectileSpeed * Time.deltaTime;
    }
}
