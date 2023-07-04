using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BossDoorColor : MonoBehaviour
{
    private Tilemap tile;

    private Animator anim;
    
    private float val = 0f;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Open()
    {
        anim.SetTrigger("Open");
    }
    public void Close()
    {
        anim.SetTrigger("Close");
    }
    
}
