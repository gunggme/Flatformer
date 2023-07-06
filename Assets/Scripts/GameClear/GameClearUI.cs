using System;
using TMPro;
using UnityEngine;

public class GameClearUI : MonoBehaviour
{
    [SerializeField] private TMP_Text time;

    private void Start()
    {
        Debug.Log(GameManager.instance.saveManager.rank.time);
        TimeSpan t = TimeSpan.FromSeconds(GameManager.instance.saveManager.rank.time);
        time.text = $"{t.Hours:D2}:{t.Minutes:D2}:{t.Seconds:D2}";
    }
}
