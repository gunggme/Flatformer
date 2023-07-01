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

    public GameObject parent;
    
    private bool isSpawn;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        parent.SetActive(false);
    }
    public void BossSpawn()
    {
        Debug.Log("BossSpawn");
        coll.isTrigger = true;
        parent.SetActive(true);
        transform.position = new Vector2(190, 60);
        rigid.bodyType = RigidbodyType2D.Kinematic;
        // rigid.velocity = Vector2.down * 10;
    }

    private void FixedUpdate()
    {
        if (!isSpawn && rigid.bodyType == RigidbodyType2D.Dynamic && rigid.velocity.y == 0)
        {
            isSpawn = true;
            anim.SetTrigger("StopFall");
        }
        if (isSpawn)
        {
            Move();
        }
    }

    private void Move()
    {
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        rigid.bodyType = RigidbodyType2D.Dynamic;
        coll.isTrigger = false;
    }
}
