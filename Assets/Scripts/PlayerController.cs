using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 5;
    [SerializeField] private float runSpeed = 8;
    [SerializeField] private float airWalkSpeed = 3f;
    [SerializeField] private float jumpImpulse = 10 ;
    [SerializeField] private bool isDash;
    [SerializeField] private float dashPower;
    private WaitForSeconds waitDash = new(1);
    private Vector2 moveInput;
    private TouchingDirection touchingDirection;
    private Damageable damageable;
    [SerializeField] private float dashTime = 3f;

    public bool IsDash
    {
        get
        {
            return isDash;
        }

        set
        {
            isDash = value;
            animator.SetBool(AnimationStrings.isDash, value);
        }
    }
    
    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && !touchingDirection.IsOnWall)
                {
                    if (touchingDirection.IsGrounded)
                    {
                        if (IsRunning && !IsDash)
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }
                    }
                    else
                    {
                        // Aire Move 0070
                        return airWalkSpeed;
                    }
                }
                else
                {
                    // Idle Speed is 0
                    return 0;
                }
            }

            else
            {
                return 0;
            }
        }
    }
    
    [SerializeField]
    private bool _isMoving;
    
    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        set
        {
            
            _isMoving = value;    
            
            
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }


    [SerializeField] 
    private bool _isRunning = false;

    public bool IsRunning
    {
        get
        {

            return _isRunning;
        }
        set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    public bool _isFacingRight = true;

    public bool IsFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        set
        {
            if (_isFacingRight != value)
            {
                // Flip the local scale to make the player face the opposite direction
                transform.localScale *= new Vector2(-1, 1); 
            }
            
            _isFacingRight = value;
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }

    private Rigidbody2D rigid;
    private Animator animator;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirection = GetComponent<TouchingDirection>();
        damageable = GetComponent<Damageable>();
    }

    private void Update()
    {
        if (IsDash)
        {
            IsMoving = false;
        }
    }

    private void FixedUpdate()
    {
        if (!damageable.LockVelocity && !IsDash)
        {
            rigid.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rigid.velocity.y);
        }
        
        animator.SetFloat(AnimationStrings.yVelocity, rigid.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        
        moveInput = context.ReadValue<Vector2>();

        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;

            SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }
    }

    private void SetFacingDirection(Vector2 moveInput)
    {

        if (moveInput.x > 0 && !IsFacingRight)
        {
            // Face the right
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight )
        {
            // Face the left
            IsFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // TODO check if alive as well
        if (context.started && touchingDirection.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rigid.velocity = new Vector2(rigid.velocity.x, jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context) 
    {
        if (context.started && !IsDash)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }

    public void OnRageAttack(InputAction.CallbackContext context) 
    {
        if (context.started && !IsDash)
        {
            animator.SetTrigger(AnimationStrings.rangeAttackTrigger);
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if(!isDash && touchingDirection.IsGrounded && IsRunning)
                StartCoroutine(DashCou());
        }
    }
    
    // Dash
    IEnumerator DashCou()
    {
        IsDash = true;
        
        rigid.velocity = new Vector2(dashPower * transform.localScale.x, rigid.velocity.y);
        
        yield return new WaitForSeconds(dashTime);
        
        IsDash = false;
        
    }

    
    public void OnHit(int damage, Vector2 knockback)
    {
        
        rigid.velocity = new Vector2(knockback.x, rigid.velocity.y + knockback.y);
    }
}
