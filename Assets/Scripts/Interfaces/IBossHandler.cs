using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBossHandler 
{
    public void Initialize();
    public void UpdateHealth();
    public void HandleDeath();
    public void TakeDamage(int _damage, bool _isdot, int _dotAmount);
    public HealthSystem GetHealthSystem();
    public StatSystem GetStatSystem();
    public BossBrain[] GetBrains();
}
