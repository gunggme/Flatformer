using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class BossKnight : MonoBehaviour
{
    private Rigidbody2D rigid;
    private CapsuleCollider2D coll;
    private Animator anim;
    
    // public DetectionZone detectZone;
    // public DetectionZone reverseDetectZone;
    public GameObject player;

    private bool isSpawn;

    private int _attackCnt = 0;
    private int AttackCnt
    {
        get
        {
            return _attackCnt;
        }
        set
        {
            _attackCnt = value;
            if (_attackCnt > 2)
                _attackCnt = 0;

        }
    }
    private int[] attackAnimation =
        { Animator.StringToHash("Attack1"), Animator.StringToHash("Attack2"), Animator.StringToHash("Attack3") };
    
    public void BossSpawn()
    {
        Debug.Log("BossSpawn");
        coll.isTrigger = true;
        transform.position = new Vector2(180, 50);
        gameObject.SetActive(true);
        rigid.velocity = Vector2.down;
    }
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (rigid.velocity.y != 0)
            return;
        if (!isSpawn)
        {
            isSpawn = true;
            anim.SetTrigger("StopFall");
            return;
        }

        Roll();
    }

    private readonly int roll = Animator.StringToHash("Roll");
    private bool isRollin = false;
    private float rollCoolTime = 0f;
    private void Roll()
    {
        if (rollCoolTime > 0f)
        {
            rollCoolTime -= Time.fixedDeltaTime;
            return;
        }
        
        if (isRollin)
        {
            // Debug.Log(anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
            // Debug.Log(anim.GetCurrentAnimatorStateInfo(0).IsName("HeroKnight_Roll"));
            rigid.velocity = transform.localScale.x * Time.fixedDeltaTime * 150f * Vector2.right;
        }
        
        
        if (Vector2.Distance(transform.position, player.transform.position) > 8.5f && !isRollin)
        {
            rollCoolTime = 5f;
            anim.SetTrigger(roll);
            isRollin = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        coll.isTrigger = false;
    }
}
