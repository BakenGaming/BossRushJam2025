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
    [SerializeField] private RectTransform _endLeft;
    [SerializeField] private RectTransform _startRight;
    [SerializeField] private Vector3 staticMovementVector;
    [SerializeField] private Transform optionLocation;

    private Vector3 movementVector;
    private GameObject objectToCenter;
    private int positionIndex=0, positionOffsetX = 120;
    private float spinTimer, spinDurationMin=2f, spinDurationMax=3f;
    private bool spin=false, spinDown=false, checkingDistance;
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
        movementVector = staticMovementVector;
        objectToCenter = null;
        checkingDistance = false;
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
        _startRight.anchoredPosition = new Vector2(positionIndex-positionOffsetX, 0);
        
    }

    private void ResetRoulette()
    {
        foreach(GameObject _option in availableOptions)
        {
            Destroy(_option);
        }

        availableOptions.Clear();
        availableOptions = new List<GameObject>();
        positionIndex = 0;
    }
    private void Update() 
    {
        if(spin)
        {
            foreach(GameObject _option in availableOptions)
            {
                _option.GetComponent<RectTransform>().localPosition += movementVector * Time.deltaTime;
                if(_option.GetComponent<RectTransform>().localPosition.x <= _endLeft.localPosition.x)
                    _option.GetComponent<RectTransform>().localPosition = _startRight.localPosition;
            }
            
        }  
        UpdateTimers();  
    }

    private void UpdateTimers()
    {
        if(spin) {spinTimer -= Time.deltaTime; spinDown = true; }

        if(spinTimer <=0 && spinDown) {movementVector.x += 5f;}

        if(movementVector.x >= 0) {spinDown = false; spin = false; CheckDistancesAndAdjust();}
    }

    private void CheckDistancesAndAdjust()
    {
        if(checkingDistance) return;

        checkingDistance = true;
        positionIndex = positionOffsetX;
        
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
                _option.GetComponent<RectTransform>().localPosition = new Vector3(positionIndex, 0);
                Debug.Log($"Placing at {positionIndex}");
                positionIndex += positionOffsetX;
            }    
        }
        objectToCenter.GetComponent<WeaponOption>().SelectWeapon();
    }
}
