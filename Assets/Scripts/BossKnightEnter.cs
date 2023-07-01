using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKnightEnter : MonoBehaviour
{
    [SerializeField] private BossKnight bossKt;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Room Enter!");
        bossKt.BossSpawn();
        Destroy(gameObject);
    }
}
