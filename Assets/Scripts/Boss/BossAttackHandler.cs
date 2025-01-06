using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackHandler : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    private bool isBombing;
    private bool isShooting;
    private float timeBetweenShots = 1f, shotTimer;

    public void Initialize()
    {
        BossMovement.OnMovementPaused += ActivateBomb;
        isBombing = false;
        isShooting = false;
        shotTimer = 0;
    }

    private void OnDisable() 
    {
        BossMovement.OnMovementPaused -= ActivateBomb;
    }

    public void BombPlayer()
    {
        if(isBombing)
        {
            Projectile newProjectile = ObjectPooler.DequeueObject<Projectile>("Berry Bomb");
            newProjectile.transform.position = firePoint.position;
            newProjectile.gameObject.SetActive(true);
            newProjectile.InitializeBomb(GetComponent<EnemyHandler>().GetEnemyStatsSO(), GameManager.i.GetPlayerGO().transform.position, "Berry Bomb");
            isBombing = false;
        }
    }

    public void ShootPlayer()
    {
        if(isShooting)
        {
            Projectile newProjectile = ObjectPooler.DequeueObject<Projectile>("Berry");
            newProjectile.transform.position = firePoint.position;
            newProjectile.gameObject.SetActive(true);
            newProjectile.InitializeNormalShot(GetComponent<EnemyHandler>().GetEnemyStatsSO(), GameManager.i.GetPlayerGO().transform.position, "Berry");
            isShooting = false;
            shotTimer = timeBetweenShots;
        }
        else UpdateTimer();
    }

    private void UpdateTimer() 
    {
        shotTimer -= Time.deltaTime;
        if(shotTimer <= 0) isShooting = true;    
    }

    private void ActivateBomb(){isBombing = true;}
}
