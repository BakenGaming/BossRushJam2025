using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Splash : MonoBehaviour
{
    [SerializeField] private Sprite[] possibleSprites;

    private SpriteRenderer _sr;
    private CircleCollider2D _collider;
    private float disappearTimer = 3f;
    private Color spriteColor;
    private float tickTimer, tickTime = 1f;
    private bool _isInSplash, _canDamage;

    public void Initialize()
    {
        _sr = GetComponent<SpriteRenderer>();
        _collider = GetComponent<CircleCollider2D>();
        _sr.sprite = possibleSprites[Random.Range(0, possibleSprites.Length)];
        spriteColor = _sr.color;
        tickTimer = 0;  
        _isInSplash = false;  
    }

    private void Update() 
    {
        Decay();

        if(_isInSplash)
        {
            tickTimer -= Time.deltaTime;
            if(tickTimer <= 0)
                _canDamage = true;
        }    
    }

    private void Decay()
    {
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            // Start disappearing
            _collider.enabled = false;
            float disappearSpeed = 1f;
            spriteColor.a -= disappearSpeed * Time.deltaTime;
            _sr.color = spriteColor;
            if (spriteColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D _trigger) 
    {
        IDamageable damageable = _trigger.GetComponent<IDamageable>();
        if(damageable != null && _trigger.tag == "Player")
            _isInSplash = true;
    }
    private void OnTriggerStay2D(Collider2D _trigger) 
    {
        IDamageable damageable = _trigger.GetComponent<IDamageable>();
        if(damageable != null && _canDamage)
        {
            damageable.TakeDamage(1,false,0);
            tickTimer = tickTime;
            _canDamage = false;
        }
    }

    private void OnTriggerExit2D(Collider2D _trigger) 
    {
        if(_trigger.tag == "Player") 
        {
            tickTimer = 0;
            _isInSplash = false;
        }
    }
}
