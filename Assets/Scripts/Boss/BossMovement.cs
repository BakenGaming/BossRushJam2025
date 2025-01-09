using UnityEngine;
using System;


public class BossMovement : MonoBehaviour
{
    public static event Action OnMovementPaused;
    private float xMin = -20f, xMax = 20f, yMin = -8f, yMax = 10f, movementWaitTimer, movementWaitTime = 1f;
    private Transform movePoint;
    private bool _isMoving = false, isInitialized = false, _isWaiting = false;
    public void RandomMovement()
    {
        if(!isInitialized) return;
        UpdateTimers();
        
        if(_isWaiting) return;

        if(_isMoving && !GameManager.i.GetIsPaused()) Move();
        else
        {
            movePoint.position = new Vector2(UnityEngine.Random.Range(xMin,xMax), UnityEngine.Random.Range(yMin,yMax));
            _isMoving = true;
        }
    }

    public void Initialize()
    {
        movePoint = transform.Find("MovePoint");
        movePoint.SetParent(null);
        isInitialized = true;
    }

    private void Move()
    {
        Vector3 moveDir = (movePoint.position - transform.position).normalized;

        // float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;

        // transform.eulerAngles = new Vector3(0, 0, angle - 90f);
        
        transform.position += moveDir * GetComponent<EnemyHandler>().GetEnemyStatsSO().moveSpeed * Time.deltaTime;

        if(Vector2.Distance(movePoint.position, transform.position) <= .5f)
        {
            _isMoving = false;
            PauseMovement();
            OnMovementPaused?.Invoke();
        }
    }

    private void UpdateTimers()
    {
        if(_isWaiting) movementWaitTimer -= Time.deltaTime;

        if(movementWaitTimer <= 0) _isWaiting = false;
    }
    private void PauseMovement()
    {
        _isWaiting = true;
        movementWaitTimer = movementWaitTime;
    }
}
