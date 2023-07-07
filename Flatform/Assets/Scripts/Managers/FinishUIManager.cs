using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text time;

    private void Start()
    {
        if (GameManager.instance is null)
            return;
        Debug.Log(GameManager.instance.saveManager.rank.time);
        TimeSpan t = TimeSpan.FromSeconds(GameManager.instance.saveManager.rank.time);
        time.text = $"{t.Hours:D2}:{t.Minutes:D2}:{t.Seconds:D2}";
    }

    public void GotoMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void GotoRestart()
    {
        GameManager.instance.ResetTime();
        SceneManager.LoadScene("Stage1");
    }
}
