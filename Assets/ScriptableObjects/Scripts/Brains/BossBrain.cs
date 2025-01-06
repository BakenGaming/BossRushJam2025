using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossBrain : ScriptableObject
{
    public abstract void InitializeAI(IHandler _handler);
    public abstract void Think(BossThinker _thinker);
}
