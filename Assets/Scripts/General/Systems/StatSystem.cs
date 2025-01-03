using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatSystem
{
    private int health;
    private float moveSpeed;
    private float spinRate;

    public StatSystem (PlayerStatsSO _stats)
    {
        health = _stats.baseHealth;
        moveSpeed = _stats.baseMoveSpeed;
        spinRate = _stats.baseSpinRate;

    }

    public StatSystem (EnemyStatsSO _stats)
    {
        health = _stats.health;
        moveSpeed = _stats.moveSpeed;
    }

    public void UpdateHealth(int _amount)
    {
        health += _amount;
    }
    
    public int GetHealth (){return health;}
    public float GetMoveSpeed(){return moveSpeed;}
    public float GetSpinRate(){return spinRate;}
}
