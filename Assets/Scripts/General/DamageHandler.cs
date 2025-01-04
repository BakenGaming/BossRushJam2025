using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour, IDamageable
{
    [SerializeField] private bool isTesting;
    private IHandler handler;
    private bool isEnemy;
    
    public void InitializeDamage(bool _isEnemy)
    {
        isEnemy = _isEnemy;
        if (isEnemy) handler = GetComponentInParent<EnemyHandler>(); 
        else handler = GetComponentInParent<PlayerHandler>();
    }
    public void TakeDamage(int _damage)
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
}
