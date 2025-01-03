using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusStatController
{
    #region Variables
    private int bonusHealth;
    private float bonusMoveSpeed;
    private float bonusSpinRate;
    #endregion

    #region Setup

    public BonusStatController(int _h, float _m, float _s)
    {
        bonusHealth = _h;
        bonusMoveSpeed = _m;
        bonusSpinRate = _s;
    }
    #endregion
    #region Functions
    public void UpdateBonusHealth(int _amount)
    {
        bonusHealth += _amount;
    }

    public void UpdateBonusMoveSpeed(float _amount)
    {
        bonusMoveSpeed += _amount;
    }

    public void UpdateBonusSpinRate(float _amount)
    {
        bonusSpinRate += _amount;
    }

    #endregion
    
    #region Get Functions
    public int GetBonusHealth(){return bonusHealth;}
    public float GetBonusMoveSpeed(){return bonusMoveSpeed;}
    public float GetBonusSpinRate(){return bonusSpinRate;}
    #endregion
}
