using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            if (!_instance)
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
    public SaveManager saveManager;

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
        Scene curScene = SceneManager.GetActiveScene();
        
        if (curScene.buildIndex is 0)
        {
            Destroy(poolManager.gameObject);
            Destroy(gameObject);
        }

        if (Timer < 0)
        {
            Timer = 0;
        }

        // Test
        if (Input.GetKeyDown(KeyCode.K))
            SaveDataToJson();
        
        if (curScene.name is "main" or "GameOver")
        {
            Timer = 0;
            return;
        }
        
        Timer += Time.fixedDeltaTime;
    }

    public void SaveDataToJson()
    {
        saveManager.CallRankStructSave();
    }
}
