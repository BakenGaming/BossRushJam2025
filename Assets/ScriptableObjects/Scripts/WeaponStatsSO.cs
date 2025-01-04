using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Weapon Stats")]
public class WeaponStatsSO : ScriptableObject
{
    public string weaponName;
    public string projectileName;
    public Sprite[] projectileSprite;
    public GameObject projectileGO;
    public int numberOfProjectilesPerShot;
    public float projectileSpeed;
    public int projectileDamage;
    public float lifeTime;
}
