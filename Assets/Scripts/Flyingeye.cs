using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyingeye : MonoBehaviour
{
    public float flightSpeed = 2;
    public float waypointReachedDistance = 0.1f;
    public DetectionZone biteDetectionZone;
    public Collider2D deathCollider;
    public List<Transform> waypoints;

    private Animator animator;
    private Rigidbody2D rigid;
    private Damageable damageable;

    private Transform nextWaypoint;
    private int waypointNum = 0;
    
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

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damageable>();
    }

    private void Start()
    {
        nextWaypoint = waypoints[waypointNum];
    }

    private void OnEnable()
    {
        //damageable.damageableDeath += OnDeath();
    }

    private void Update()
    {
        HasTarget = biteDetectionZone.detectedCollider.Count > 0;
    }

    private void FixedUpdate()
    {
        if (damageable.IsAlive)
        {
            if (CanMove)
            {
                Flight();
            }
            else
            {
                rigid.velocity = Vector3.zero;
            }
        }
    }

    void Flight()
    {
        // Fly to next waypoint
        Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;
        
        // Check if we have reached the waypoint already
        float distance = Vector2.Distance(nextWaypoint.position, transform.position);
        UpdateDirection();
        
        rigid.velocity = directionToWaypoint * flightSpeed;
        
        // See if we nead to switch waypoints
        if (distance <= waypointReachedDistance)
        {
            // switch to next waypoint
            waypointNum++;
            if (waypointNum >= waypoints.Count)
            {
                // Loop back to original waypoint
                waypointNum = 0;
            }

            nextWaypoint = waypoints[waypointNum];
        }
    }

    void UpdateDirection()
    {
        Vector3 locScale = transform.localScale;
        
        if (transform.localScale.x > 0)
        {
            // Facing the right
            if (rigid.velocity.x < 0)
            {
                // Flip
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
        else
        {
            // Facing the left
            if (rigid.velocity.x > 0)
            {
                // Flip
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
    }

    public void OnDeath()
    {
        rigid.gravityScale = 2f;
        rigid.velocity = new Vector2(0, rigid.velocity.y);
        deathCollider.enabled = true;
    }
}
