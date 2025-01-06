using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Brains/Random Movement Brain")]
public class RandomMovementBrain : BossBrain
{
    private bool _initialized;
    public override void InitializeAI(IHandler _handler)
    {
        _initialized = true;
    }
    public override void Think(BossThinker _thinker)
    {
        var randomMovement = _thinker.gameObject.GetComponent<BossMovement>();
        
        if(randomMovement && _initialized)
        {
            randomMovement.RandomMovement();
        }
    }
}
