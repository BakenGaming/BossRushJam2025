using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHandler
{
    public void Initialize();
    public void UpdateHealth();
    public void HandleDeath();
    public HealthSystem GetHealthSystem();
    public StatSystem GetStatSystem();
}
