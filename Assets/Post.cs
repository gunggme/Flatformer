using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Post : MonoBehaviour
{
    
    private void Start()
    {
        GameManager.instance.saveManager.CallRankStructSave();
        StartCoroutine(GameManager.instance.saveManager.UnityWebRequestPOSTTEST("pcs.pah.kr:1005/api/insert"));
    }
}
