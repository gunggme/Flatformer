using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                return null;
            }
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }
    
    public PoolManager poolManager;

    private float _timer;

    public float Timer
    {
        get
        {
            return _timer;
        }
        set
        {
            _timer = value;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Destroy(poolManager.gameObject);
            Destroy(gameObject);
        }

        if (Timer < 0)
        {
            Timer = 0;
        }

        if (SceneManager.GetActiveScene().buildIndex != 4)
        {
            Timer += Time.fixedDeltaTime;
        }                         
    }
}

public class RankData
{
    public string userName;
    public float h;
    public float m;
    public float s;
}
