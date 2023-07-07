using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;
    public Vector2 moveSpeed = new Vector2(3f, 0);
    public Vector2 knockback = new Vector2(0, 0);

    public GameObject pro;

    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Fire()
    {
        Invoke(nameof(ActiveFalse), 3);
        rigid.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }

    private void OnDisable()
    {
        rigid.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // See if it can be hit
        Damageable damageable = other.GetComponent<Damageable>();

        if (damageable != null)
        {
            Vector2 deliveredKcockback =
                transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            
            // Hit the target
            bool gotHit = damageable.Hit(damage, deliveredKcockback);

            if (gotHit)
            {
                CancelInvoke(nameof(ActiveFalse));
                gameObject.SetActive(false);
            }
        }
    }

    private void ActiveFalse()
    {
        gameObject.SetActive(false);
        transform.localScale = new Vector3(1, 1, 1);
    }
    
    
}
