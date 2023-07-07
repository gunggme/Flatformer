using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnBack : MonoBehaviour
{
    [SerializeField] private Transform targetBack;

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.transform.position = targetBack.position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.transform.position = targetBack.position;
    }
}
