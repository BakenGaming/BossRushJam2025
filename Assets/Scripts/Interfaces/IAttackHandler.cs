using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IAttackHandler
{
    public void Initialize();
    public Vector3 GetShootDirection();
    public Transform GetFirePoint();
}
