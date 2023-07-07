using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
    public UnityEvent noCollidersRemain;

    public List<Collider2D> detectedCollider = new List<Collider2D>();
    private Collider2D col;

    void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        detectedCollider.Add(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        detectedCollider.Remove(other);

        if (detectedCollider.Count <= 0)
        {
            noCollidersRemain.Invoke();
        }
    }
}
