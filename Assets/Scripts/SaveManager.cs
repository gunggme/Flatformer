using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

public class SaveManager : MonoBehaviour
{
    private string _playerName;

    public string playerName
    {
        get
        {
            return _playerName;
        }
        set
        {
            _playerName = value;
        }
    }
    
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void RankStructSave()
    {
        RankSet rank = new RankSet();

        rank.userName = playerName;
    }
}

public class RankSet
{
    public string userName;
    public float time;
}