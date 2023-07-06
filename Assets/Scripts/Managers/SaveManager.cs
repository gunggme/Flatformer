using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    [SerializeField] public RankSet rank = new RankSet();

    private string _playerName;

    private bool isPost = false;

    public string playerName
    {
        get { return _playerName; }
        set { _playerName = value; }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        
    }

    public void CallRankStructSave() => RankStructSave();

    private void RankStructSave()
    {
        rank.time = GameManager.instance.Timer;
    }

    public IEnumerator UnityWebRequestPOSTTEST(string url)
    {
        string json = JsonUtility.ToJson(rank);
        Debug.Log(json);

        var req = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        //Send the request then wait here until it returns
        yield return req.SendWebRequest();
        if (req.isNetworkError)
        {
            Debug.Log("Error While Sending: " + req.error);
            yield break;
        }
        else
        {
            Debug.Log("Received: " + req.downloadHandler.text);
            yield break;
        }
    }
}

[System.Serializable]
public class RankSet
{
    public string playerName;
    public float time;
}
