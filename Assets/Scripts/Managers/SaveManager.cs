using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public RankSet rank = new();

    private string _playerName;

    public string playerName
    {
        get { return _playerName; }
        set { _playerName = value; }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void CallRankStructSave() => RankStructSave();

    private void RankStructSave()
    {
        rank.userName = playerName;
        rank.time = GameManager.instance.Timer;

        string jsonData = JsonUtility.ToJson(rank);
        string path = Path.Combine(Application.dataPath, "rankData.json");

        File.WriteAllText(path, jsonData);
    }
}

public class RankSet
{
    public string userName;
    public float time;
}
