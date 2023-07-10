using System;
using UnityEngine;

public class Post : MonoBehaviour
{
    [Obsolete("Obsolete")]
    private void Awake()
    {
        if (!GameManager.instance)
            return;
        GameManager.instance.saveManager.CallRankStructSave();
        StartCoroutine(GameManager.instance.saveManager.UnityWebRequestPosttest("pcs.pah.kr:1005/api/insert"));
    }
}
