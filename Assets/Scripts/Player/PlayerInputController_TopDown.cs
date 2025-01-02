using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using CodeMonkey.Utils;

public class PlayerInputController_TopDown : MonoBehaviour, IInputHandler
{
    #region Events
    public static OnFireWeapon OnPlayerAttack;
    public static Action OnPauseGame;
    public static Action OnUnpauseGame;
    #endregion

    #region Variables
    public delegate void OnFireWeapon(Vector3 mousePos);
    private StatSystem _playerStats;
    private InputAction _move, _attack, _dash, _pause;
    private GameControls _controller;
    private Vector2 moveInput;
    private Rigidbody2D playerRB;
    private GameObject _playerSprite;
    private Camera mainCam;
    private Vector3 aimDirection;

    #endregion

    #region Initialize
    public void Initialize()
    {
        _playerStats = GetComponent<IHandler>().GetStatSystem();    
        _controller = new GameControls();
        _playerSprite = transform.Find("Sprite").gameObject;
        playerRB = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;
        GetComponent<AttackHandler_TopDown>().Initialize();

        _move = _controller.PlatformerControls.MoveInput;
        _move.Enable();

        _dash = _controller.PlatformerControls.Attack;
        _dash.performed += HandleDashInput;
        _dash.Enable();

        _pause = _controller.PlatformerControls.Pause;
        _pause.performed += HandlePauseInput;
        _pause.Enable();

        _attack = _controller.PlatformerControls.Attack;
        _attack.performed += HandleAttackInput;
        _attack.Enable();
    }

    private void OnDisable()
    {
       _move.Disable();
       _attack.Disable();
       _dash.Disable();
       _pause.Disable();
    }
    #endregion

    #region Input Handling
    private void HandleAttackInput(InputAction.CallbackContext context)
    {
        OnPlayerAttack(UtilsClass.GetMouseWorldPosition());
    }
    private void HandleDashInput(InputAction.CallbackContext context)
    {
        
    }
    private void HandlePauseInput(InputAction.CallbackContext context)
    {
        if(!GameManager.i.GetIsPaused()) OnPauseGame?.Invoke();
        else OnUnpauseGame?.Invoke();

    }

    #endregion

    #region Loop
    private void Update() 
    {
        if(!GameManager.i.GetIsPaused()) moveInput = _move.ReadValue<Vector2>();
        else playerRB.velocity = Vector2.zero;

        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        aimDirection = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        _playerSprite.transform.eulerAngles = new Vector3(0, 0, angle - 90f);
    }

    private void FixedUpdate() 
    {
        if(GameManager.i.GetIsPaused()) 
        {
            playerRB.velocity = Vector2.zero;
            return;
        }
        
        Vector2 moveSpeed = moveInput.normalized;
        playerRB.velocity = new Vector2(moveSpeed.x * _playerStats.GetMoveSpeed(), moveSpeed.y *.5f * _playerStats.GetMoveSpeed());    
    }

    #endregion

    #region Checks

    #endregion

    #region Get Functions
    public Vector3 GetShootDirection() { return aimDirection;}
    #endregion
}
