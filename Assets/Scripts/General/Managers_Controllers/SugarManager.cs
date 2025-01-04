using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SugarManager : MonoBehaviour
{
    public static event Action<bool, int> OnSugarDecrease;
    public static event Action<bool, int> OnSugarIncrease;
    public static event Action OnEnoughSugarCollected;
    private static SugarManager _i;
    public static SugarManager i { get { return _i; } }
    private int currentSugarCount, sugarToAddWeapon=5;
    public void Initialize()
    {
        _i = this;
        currentSugarCount = 0;
        LootObject.OnSugarCollected += AddSugar;
        Projectile.OnProjectileMiss += SubtractSugar;
    }
    private void OnDisable() 
    {
        LootObject.OnSugarCollected -= AddSugar;    
        Projectile.OnProjectileMiss -= SubtractSugar;
    }

    private void AddSugar()
    {
        currentSugarCount++;
        OnSugarIncrease?.Invoke(false, 1);
        if(currentSugarCount >= sugarToAddWeapon)
        {
            SubtractSugar(sugarToAddWeapon, true);
            sugarToAddWeapon = sugarToAddWeapon + ((int)(sugarToAddWeapon * 1.1f));
            Debug.Log(sugarToAddWeapon);
            OnEnoughSugarCollected?.Invoke();
        } 
    }

    private void SubtractSugar(int _amount, bool _isPurchase)
    {
        if(_isPurchase)
        {
            if(currentSugarCount - _amount < 0)
            {
                Debug.Log("Can't Purchase");
            }
            else 
            {
                currentSugarCount -= _amount;
                OnSugarDecrease?.Invoke(true, _amount);
            }
        }
        else
        {
            if(currentSugarCount - _amount < 0) currentSugarCount = 0;
            else 
            {
                currentSugarCount -= _amount;
                OnSugarDecrease?.Invoke(true, _amount);
            }
        }
        
    }
    public int GetCurrentSugarCount(){return currentSugarCount;}
}
