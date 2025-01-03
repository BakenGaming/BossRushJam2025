using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Weapon Stats")]
public class WeaponStatsSO : ScriptableObject
{
    public string weaponName;
    public Sprite[] projectileSprite;
    public int numberOfProjectilesPerShot;
    public float projectileSpeed;
    public float projectileDamage;
    public float lifeTime;
}
