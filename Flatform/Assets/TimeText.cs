using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeText : MonoBehaviour
{
    private TMP_Text tmpText;

    private void Awake()
    {
        tmpText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        TimeSpan t = TimeSpan.FromSeconds(GameManager.instance.Timer);
        tmpText.text = $"{t.Hours:D2}:{t.Minutes:D2}:{t.Seconds:D2}";
    }
}
