using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThinker : MonoBehaviour
{
    private BossBrain[] brain;

    public void ActivateBrain(IHandler _handler)
    {
        brain = _handler.GetBrains();
        foreach (BossBrain _brain in brain)
            _brain.InitializeAI(GetComponent<EnemyHandler>());
    }
    private void LateUpdate() 
    {
        foreach (BossBrain _brain in brain)
            _brain.Think(this);    
    }
}
