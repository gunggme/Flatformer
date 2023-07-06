using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshPro time;

    private void Awake()
    {
        // TimeSpan t = TimeSpan.FromSeconds(GameManager.instance.rankData.time);
        // time.text = $"{t.Hours:D2}:{t.Minutes:D2}:{t.Seconds:D2}";
    }
}
