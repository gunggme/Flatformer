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

    [SerializeField] private float _timer;
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

        saveManager = FindObjectOfType<SaveManager>();
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
        
        if (curScene.name is "Main")
        {
            Timer = 0;
            return;
        }

        if (curScene.name != "GameOver" && curScene.name != "Main")
        {
            Timer += Time.deltaTime;
        }

        // Test
        /*if (Input.GetKeyDown(KeyCode.F8))
        {
            SceneManager.LoadScene("GameClear");
        }*/
    }

    public void ResetTime()
    {
        Timer = 0;
    }
}
