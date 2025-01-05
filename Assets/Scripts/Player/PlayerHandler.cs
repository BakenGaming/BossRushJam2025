using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerHandler : MonoBehaviour, IHandler
{
    #region Events
    public static event Action onDamageReceived;
    #endregion
    #region Variables
    [SerializeField] private PlayerStatsSO playerStatsSO;

    private StatSystem _statSystem;
    private HealthSystem _healthSystem;

    #endregion
    #region Initialize
    public void Initialize()
    {
        SetupPlayer();
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

    #region Player Setup
    private void SetupPlayer()
    {
        _statSystem = new StatSystem(playerStatsSO);
        _healthSystem = new HealthSystem(_statSystem.GetHealth());
        _healthSystem.IncreaseMaxHealth(GameManager.i.GetBonusStats().GetBonusHealth());
        
        GetComponent<IInputHandler>().Initialize();
        GetComponent<IAttackHandler>().Initialize();
        GetComponent<IDamageable>().InitializeDamage(false);
    }
    #endregion

    #region Loop
    private void Update() {
#if UNITY_EDITOR
    if (Input.GetKeyDown(KeyCode.F))
            TakeDamage(5, false, 0);
    
    if (Input.GetKeyDown(KeyCode.G))
        TakeDamage(5, true, 2);
            
#endif        
    }
    #endregion
}
