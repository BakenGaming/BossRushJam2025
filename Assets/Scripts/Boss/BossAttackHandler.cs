using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackHandler : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    private bool isBombing;

    public void Initialize()
    {
        BossMovement.OnMovementPaused += ActivateBomb;
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
            newProjectile.Initialize(GetComponent<EnemyHandler>().GetEnemyStatsSO(), GameManager.i.GetPlayerGO().transform.position, "Berry Bomb");
            isBombing = false;
        }
    }

    private void ActivateBomb(){isBombing = true;}
}
