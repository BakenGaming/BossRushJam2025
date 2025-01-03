using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void InitializeDamage(bool _isEnemy);
    public void TakeDamage(int _damage);
}
