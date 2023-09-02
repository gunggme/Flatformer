 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirection : MonoBehaviour
{
    [SerializeField] private ContactFilter2D castFilter;
    [SerializeField] private float groundDistance = 0.05f;
    [SerializeField] private float wallDistance = 0.2f;

    CapsuleCollider2D touchingCol;

    private Animator animator;
    
    private RaycastHit2D[] groundHits = new RaycastHit2D[5];
    private RaycastHit2D[] wallHits = new RaycastHit2D[5];
    [SerializeField]
    private bool _isGrounded;

    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
        set
        {
            _isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded, value);
        }
    }
    
    [SerializeField]
    private bool _isOnWall;

    public bool IsOnWall
    {
        get
        {
            return _isOnWall;
        }
        set
        {
            _isOnWall = value;
            animator.SetBool(AnimationStrings.isOnWall, value);
        }
    }

    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
    
    void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        IsGrounded =  touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsOnWall = touchingCol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
    }
}
