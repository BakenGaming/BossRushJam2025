using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Brains/Berry Bomb Brain")]
public class BerryBombBrain : BossBrain
{
    private Vector3 playerLastPosition;
    public override void InitializeAI(IHandler _handler)
    {
        Debug.Log("Initializing");
        playerLastPosition = GameManager.i.GetPlayerGO().transform.position;
    }

    public override void Think(BossThinker _thinker)
    {
        var attack = _thinker.gameObject.GetComponent<BossAttackHandler>();
        
        if(attack)
        {
            attack.BombPlayer();
        }
    }

}
