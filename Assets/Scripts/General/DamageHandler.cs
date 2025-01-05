using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour, IDamageable
{
    #region Variables
    [SerializeField] private bool isTesting;
    private IHandler handler;
    private bool isEnemy;
    private bool isDotActive;
    private float dotTimer, dotTime = 3f, dotIntervalTimer, dotInterval = .5f;
    private int dotDamage;
    #endregion
    #region Setup
    public void InitializeDamage(bool _isEnemy)
    {
        isEnemy = _isEnemy;
        if (isEnemy) handler = GetComponentInParent<EnemyHandler>(); 
        else handler = GetComponentInParent<PlayerHandler>();
    }
    #endregion
    #region Handle Damage
    public void TakeDamage(int _damage, bool _isDot, int _dotAmount)
    {
        if(_isDot)
            isDotActive = true;
        
        if(isDotActive)
        {
            dotTimer = dotTime;
            dotDamage = _dotAmount;
        }
        DamageEntity(_damage);
    }

    private void DamageEntity(int _damage)
    {
        handler.GetHealthSystem().LoseHealth(_damage);
        handler.UpdateHealth();
        
        if(handler.GetHealthSystem().GetCurrentHealth() <= 0) HandleDeath();

        DamagePopup.Create(transform.position, _damage);
        if(!isEnemy) Instantiate(GameAssets.i.pfPlayerDamageParticles, transform.position, Quaternion.identity);
    }

    private void HandleDeath()
    {
        if(isTesting)
        {
            handler.GetHealthSystem().RestoreHealth(0,true);
            handler.UpdateHealth();
        }
        else
        {
            handler.HandleDeath();
            if(isEnemy) Destroy(gameObject);
        }
    }
    #endregion
    #region Loop
    private void Update() 
    {
        if(isDotActive)
        {
            dotTimer -= Time.deltaTime;
            if(dotTimer < 0) isDotActive = false;

            dotIntervalTimer -= Time.deltaTime;
            if(dotIntervalTimer <= 0 && dotTimer > 0) 
            {
                dotIntervalTimer = dotInterval;
                DamageEntity(dotDamage);
            }
        }    
    }
    #endregion
}
