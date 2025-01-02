using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputHandler
{
    public void Initialize();
    public Vector3 GetShootDirection();
}
