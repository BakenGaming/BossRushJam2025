using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyHandler : MonoBehaviour, IHandler
{
    #region Events
    public static event Action onDamageReceived;
    #endregion
    #region Variables
    [SerializeField] private EnemyStatsSO enemyStatsSO;

    private StatSystem _statSystem;
    private HealthSystem _healthSystem;
    #endregion
    #region Initialize

    public void Initialize()
    {
        SetupEnemy();
    }

    #endregion

    #region Get Functions
    public HealthSystem GetHealthSystem()
    {
        return _healthSystem;
    }

    public StatSystem GetStatSystem()
    {
        return _statSystem;
    }

    #endregion

    #region Handle Player Functions

    public void HandleDeath()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateHealth()
    {
        onDamageReceived?.Invoke();
    }

    public void TakeDamage(int _damage, bool _isDot, int _dotAmount)
    {
        GetComponent<IDamageable>().TakeDamage(_damage, _isDot, _dotAmount);
    }
    
    #endregion

    #region Enemy Setup
    private void SetupEnemy()
    {
        _statSystem = new StatSystem(enemyStatsSO);
        _healthSystem = new HealthSystem(_statSystem.GetHealth());
        _healthSystem.IncreaseMaxHealth(GameManager.i.GetBonusStats().GetBonusHealth());
        
    //  GetComponent<IAttackHandler>().Initialize();
        GetComponent<IDamageable>().InitializeDamage(true);
        GetComponent<LootBag>().Initialize();
    }
    #endregion
    #region Get Functions
    public EnemyStatsSO GetEnemyStatsSO(){return enemyStatsSO;}
    #endregion
}
