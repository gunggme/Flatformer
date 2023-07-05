using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossReach : MonoBehaviour
{
    public float walkAcceleration = 2;
    public float maxSpeed = 3f;
    public float walkStopRate = 0.6f;
    public DetectionZone attackZone;
    public DetectionZone reverseAttackZone;
    public DetectionZone ciffDetectionZone;

    public GameObject portal;

    private TouchingDirection touchingDirection;
    private Rigidbody2D rigid;
    private Animator animator;
    private Damageable damageable;

    public Transform[] summonPoint;
    
    public enum WalkableDirection { Right, Left }
    
    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;
    
    public WalkableDirection WalkDirection
    {
        get
        {
            return _walkDirection;
        }
        set
        {
            if (_walkDirection != value)
            {
                // Direction flipped
                gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * -1,
                    gameObject.transform.localScale.y, 0);
            }

            if (value == WalkableDirection.Right)
            {
                walkDirectionVector = Vector2.right;
            }
            else if (value == WalkableDirection.Left)
            {
                walkDirectionVector = Vector2.left;
            }

            _walkDirection = value;
        }
    }

    public bool _hasTarget;

    public bool HasTarget
    {
        get
        {
            return _hasTarget;
        }
        set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public float AttackCooldown
    {
        get
        {
            return animator.GetFloat(AnimationStrings.attackCoolDown);
        }
        set
        {
            animator.SetFloat(AnimationStrings.attackCoolDown, Mathf.Max(value, 0));
        }
    }

    void Awake()
    {

        rigid = GetComponent<Rigidbody2D>();
        touchingDirection = GetComponent<TouchingDirection>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }
    
    void Update()
    {
        HasTarget = attackZone.detectedCollider.Count > 0;
        if (reverseAttackZone.detectedCollider.Count > 0)
        {
            FlipDirection();
        }
        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if (touchingDirection.IsOnWall && touchingDirection.IsOnWall) 
        {
            FlipDirection();
        }

        if (!damageable.LockVelocity)
        {
            if (CanMove && touchingDirection.IsGrounded)
            {
                // Accelerate towards max Speed

                rigid.velocity = new Vector2(Mathf.Clamp(rigid.velocity.x + (walkAcceleration + walkDirectionVector.x * Time.fixedDeltaTime), -maxSpeed, maxSpeed) * walkDirectionVector.x, rigid.velocity.y);
            }
            else
            {
                rigid.velocity = new Vector2(Mathf.Lerp(rigid.velocity.x , 0, walkStopRate), rigid.velocity.y);
            }
        }
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right; 
        }
        else
        {
            //Debug.LogError("Current walkable direction is not set to legal values of right or left");
        }
        
    }

    public void Summoning()
    {
        foreach (var sum in summonPoint)
        {
            GameObject obj = GameManager.instance.poolManager.Get(2).gameObject;
            obj.transform.position = sum.position;
        }
    }

    public void SummonPortal()
    {
        portal.gameObject.transform.position = transform.position;
        portal.gameObject.SetActive(true);
    }

    public void OnHit(int damage, Vector2 knockback) 
    {
        rigid.velocity = new Vector2(knockback.x, rigid.velocity.y * knockback.y);
    }

    public void OnCiffDetected()
    {
        if (touchingDirection.IsGrounded)
        {
            FlipDirection();
        }
    }
}
