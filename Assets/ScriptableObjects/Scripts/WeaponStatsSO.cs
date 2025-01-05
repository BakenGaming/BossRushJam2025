using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Classification
{
    normal, seeking, shotgun, spread, dot, burst
}
[CreateAssetMenu(menuName ="Weapon Stats")]
public class WeaponStatsSO : ScriptableObject
{
    public string weaponName;
    public string projectileName;
    public Sprite weaponSprite;
    public Sprite[] projectileSprite;
    public GameObject projectileGO;
    public Classification classification;
    public int dotAmount;
    public int numberOfProjectilesPerShot;
    public float projectileSpeed;
    public int projectileDamage;
    public float lifeTime;
    public bool loseSugarOnMiss;
}
