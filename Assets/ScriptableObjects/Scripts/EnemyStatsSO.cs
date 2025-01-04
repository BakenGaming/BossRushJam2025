using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Enemy Stats")]
public class EnemyStatsSO : ScriptableObject
{
    public string enemyName;
    public int health;
    public float moveSpeed;
}
