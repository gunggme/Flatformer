using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Vector2 beforePos;

    private void Awake()
    {
        beforePos = transform.position * (Vector2.up * 7);
        transform.position = beforePos;
    }

    private void Update()
    {
        Vector2 targetPos = target.position;

        Vector2 dir = targetPos - beforePos;

        transform.position = dir/2f + (Vector2.up * 7);
        
        beforePos = transform.position;
    }
}