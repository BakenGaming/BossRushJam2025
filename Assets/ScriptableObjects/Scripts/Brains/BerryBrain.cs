using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Brains/Berry Brain")]
public class BerryBrain : BossBrain
{
    private Vector3 playerLastPosition;
    public override void InitializeAI(IHandler _handler)
    {
        playerLastPosition = GameManager.i.GetPlayerGO().transform.position;
    }

    public override void Think(BossThinker _thinker)
    {
        var attack = _thinker.gameObject.GetComponent<BossAttackHandler>();
        
        if(attack)
        {
            attack.ShootPlayer();
        }
    }
}
