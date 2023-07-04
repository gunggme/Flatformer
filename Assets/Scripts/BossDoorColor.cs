using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BossDoorColor : MonoBehaviour
{
    private Tilemap tile;
    
    private bool isEntered = false;

    private void Awake()
    {
        tile = GetComponent<Tilemap>();
    }

    public void ChangeCol()
    {
        isEntered = true;
    }

    private void Update()
    {
        if (!isEntered)
            return;
        Color col = tile.color;
        col.a += 0.002f;

        tile.color = col;
        if (col.a >= 1)
            isEntered = false;
    }
}
