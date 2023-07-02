using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKnightAnim : MonoBehaviour
{
    private Rigidbody2D rigid;

    private BossKnight bossKnight;
    
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        
        bossKnight = GetComponent<BossKnight>();
    }

    public void StopRoll()
    {
        rigid.velocity = Vector2.zero;
        bossKnight.IsRollin = false;
    }
}
