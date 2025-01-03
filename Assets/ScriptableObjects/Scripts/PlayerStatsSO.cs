using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Player Stats")]
public class PlayerStatsSO : ScriptableObject
{
    public int baseHealth;
    public float baseMoveSpeed;
    public float baseSpinRate;
}
