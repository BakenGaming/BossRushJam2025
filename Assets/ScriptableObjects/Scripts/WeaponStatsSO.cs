using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Weapon Stats")]
public class WeaponStatsSO : ScriptableObject
{
    public string weaponName;
    public GameObject weaponPrefab;
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public float projectileDamage;
}
