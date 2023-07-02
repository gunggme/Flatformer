using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKnightEnter : MonoBehaviour
{
    [SerializeField] private BossKnight bossHK;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Room Enter!");
        bossHK.BossSpawn();
        Destroy(gameObject);
    }
}
