using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class Roulette : MonoBehaviour
{
    public static event Action OnSpinWheelActive;
    [SerializeField] private List<GameObject> availableOptions;
    [SerializeField] private RectTransform _left;
    [SerializeField] private RectTransform _right;
    [SerializeField] private Transform optionLocation;
    [SerializeField] private Vector3 staticMovementVector;
    [SerializeField] private float staticSpinRate;

    private float spinRate;
    private GameObject objectToCenter, objectToPlaceAfter;
    private int positionIndex=0, positionOffsetX = 120;
    private float spinTimer, spinDurationMin=20f, spinDurationMax=30f;
    private bool spin=false, spinDown=false, checkingDistance, wheelActive, centerSet;
    private void Start() 
    {
        SugarManager.OnEnoughSugarCollected += SpinTheWheel;
    }
    private void OnDisable() 
    {
        SugarManager.OnEnoughSugarCollected -= SpinTheWheel;    
    }

    public void SpinTheWheel()
    {
        if(spin) return;
        SetupTheWheel();
        spin = true;
        spinTimer = UnityEngine.Random.Range(spinDurationMin, spinDurationMax);
        spinRate = staticSpinRate;
        objectToCenter = null;
        objectToPlaceAfter = null;
        checkingDistance = false;
        wheelActive = true;
        centerSet = false;
        Debug.Log("Ready To Spin");
    }

    private void SetupTheWheel() 
    {
        ResetRoulette();

        int wheelOptions = UnityEngine.Random.Range(4, WeaponSystem.i.GetWeapons().Length);
        for(int i = 0; i < wheelOptions; i++)
        {
            GameObject newWeaponOption = Instantiate(GameAssets.i.pfWeaponOptionRoulette, optionLocation);
            GameObject _randomWeapon = WeaponSystem.i.GetWeapons()[UnityEngine.Random.Range(0, WeaponSystem.i.GetWeapons().Length)];
            newWeaponOption.GetComponent<WeaponOption>().Initialize(_randomWeapon);
            availableOptions.Add(newWeaponOption);
        }
        
        foreach(GameObject _option in availableOptions)
        {
            _option.GetComponent<RectTransform>().localPosition = new Vector3(positionIndex, 0);
            positionIndex += positionOffsetX;
        }
        _right.anchoredPosition = new Vector2(positionIndex - positionOffsetX, 0);
        
    }

    private void ResetRoulette()
    {
        foreach(GameObject _option in availableOptions)
        {
            Destroy(_option);
        }

        availableOptions.Clear();
        availableOptions = new List<GameObject>();
        positionIndex = -120;
    }
    private void Update() 
    {
        if(spin)
        {
            foreach(GameObject _option in availableOptions)
            {
                _option.GetComponent<WeaponOption>().Move(1, _left, _right);
                // _option.GetComponent<RectTransform>().localPosition += staticMovementVector * (spinRate * Time.deltaTime);
                // if(_option.GetComponent<RectTransform>().localPosition.x <= _left.localPosition.x)
                //     SetNewPosition(_option);
            }
            if(spinTimer <=0 && spinDown) {spinRate -= .1f;}
            
        }  
        UpdateTimers();  
    }

    private void SetNewPosition(GameObject _o)
    {
        foreach(GameObject _option in availableOptions)
        {
            if(objectToPlaceAfter == null)
                objectToPlaceAfter = _o;
            else if(Mathf.Abs(_option.GetComponent<RectTransform>().localPosition.x) > 
                Mathf.Abs(objectToPlaceAfter.GetComponent<RectTransform>().localPosition.x))
                {
                    objectToPlaceAfter = _option;
                }
        }
        _o.GetComponent<RectTransform>().localPosition = new Vector3(
            objectToPlaceAfter.GetComponent<RectTransform>().localPosition.x + (objectToPlaceAfter.GetComponent<RectTransform>().rect.width /2) + positionOffsetX,0,0);
    }

    private void UpdateTimers()
    {
        if(!wheelActive) return;

        if(spin) {spinTimer -= Time.deltaTime; spinDown = true; }

        if(spinRate <= 0) {spinDown = false; spin = false; CheckDistancesAndAdjust();}
    }

    private void CheckDistancesAndAdjust()
    {
        if(checkingDistance) return;

        checkingDistance = true;
        positionIndex = -120;
        
        foreach(GameObject _option in availableOptions)
        {
            if(objectToCenter == null)
                objectToCenter = _option;
            else if(Mathf.Abs(_option.GetComponent<RectTransform>().localPosition.x) < 
                Mathf.Abs(objectToCenter.GetComponent<RectTransform>().localPosition.x))
                {
                    objectToCenter = _option;
                }
        }
        foreach(GameObject _option in availableOptions)
        {
            if(_option == objectToCenter)
                objectToCenter.GetComponent<RectTransform>().localPosition = Vector3.zero;
            else
            {
                if(positionIndex == 0) positionIndex += positionOffsetX;
                _option.GetComponent<RectTransform>().localPosition = new Vector3(positionIndex, 0);
                Debug.Log($"Placing at {positionIndex}");
                positionIndex += positionOffsetX;
            }    
        }
        objectToCenter.GetComponent<WeaponOption>().SelectWeapon();
        wheelActive = false;
    }
}
