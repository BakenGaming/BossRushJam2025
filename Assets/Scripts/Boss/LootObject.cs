using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LootObject : MonoBehaviour, ICollectable
{
    public static event Action OnSugarCollected;
    private Vector3 _target;
    private float moveSpeed = 15f, _delayTimer;
    private bool playerFound, stopDrag;
    private Rigidbody2D _rb;

    public void Initialize()
    {
        _delayTimer = 1f;
        _rb = GetComponent<Rigidbody2D>();
        float dropForce = 5000f;
        Vector2 dropDirection = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
        _rb.AddForce(dropDirection * dropForce);
    }
    public void Collect()
    {
        TextPopUp.Create(transform.position, "+1", false);
        ObjectPooler.EnqueueObject(this, "Sugar Cube");
        OnSugarCollected?.Invoke();
        
    }

    public void SetTarget(Vector3 targetPosition)
    {
        _target = targetPosition;
        playerFound = true;
    }

    void Update()
    {
        if(_delayTimer > 0) UpdateTimers();
        if(!stopDrag) UpdateDrag();
    }

    private void FixedUpdate()
    {
        if(GameManager.i.GetIsPaused()) 
        {
            _rb.velocity = Vector3.zero;
            return;
        }
        if (playerFound)
        {
            if(_delayTimer <= 0)
            {   
                stopDrag = true; 
                _rb.drag = 0f;
                Vector3 targetDirection = (_target - transform.position).normalized;
                _rb.velocity = new Vector2(targetDirection.x, targetDirection.y) * moveSpeed;
            }
        }
    }

    private void UpdateTimers(){_delayTimer -= Time.deltaTime;}
    private void UpdateDrag(){_rb.drag += .01f;}
}
