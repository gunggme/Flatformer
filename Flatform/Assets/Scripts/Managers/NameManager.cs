using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NameManager : MonoBehaviour
{
    [SerializeField] private InputField nameInput;
    [SerializeField] private SaveManager saveManager;

    [Header("BackBoard")] 
    [SerializeField] private GameObject main1Image;
    [SerializeField] private GameObject main2Image;
    [SerializeField] private GameObject rankImage;

    [SerializeField] private TMP_Text[] ranksTexts;
    [SerializeField] private int maxRanks;

    private bool isClick = false;
    
    private void Awake()
    {
        saveManager = FindObjectOfType<SaveManager>();
    }

    
    private void Update()
    {
        if (Input.anyKey && !isClick)
        {
            main1Image.SetActive(false);
            main2Image.SetActive(true);
            isClick = true;
        }
        
        if (nameInput.text.Length > 1)
        {
            saveManager.rank.playerName = nameInput.text;
        }
        else
        {
            saveManager.rank.playerName = "Anonymous" + UnityEngine.Random.Range(100, 99999);
        }
    }
    
    
    // Button Event
    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }

    public void MoveRank()
    {
        main2Image.SetActive(false);
        rankImage.SetActive(true);
        LoadRanks();
    }

    public void MoveMain()
    {
        rankImage.SetActive(false);
        main2Image.SetActive(true);
    }

    public void LoadRanks()
    {
        for (int i = 0; i < 4; i++)
        {
            ranksTexts[i].text = ""; // 초기화
        }
        StartCoroutine(SetRanks());
    }

    private IEnumerator SetRanks()
    {
        UnityWebRequest request = UnityWebRequest.Get("pcs.pah.kr:1005/api/ranking");
        yield return request.SendWebRequest();

        string rawData = request.error == null ? request.downloadHandler.text : "error";
        
        if(rawData=="error")
            Debug.Log(request.downloadHandler.error);

        List<RankSet> rankDatas = new();
        string slicedData = "";
        // 쓸데없는 정보들 다 버리고 content 안에 있는 내용만 추출
        for (int i = 12; rawData[i-1] != ']' && rankDatas.Count < maxRanks; i++) // 마지막 요소이거나, maxRanks명 만큼 뽑았을 때 break
        {
            slicedData += rawData[i];
            if (rawData[i] == '}') // '}'이 나왔다는 것은 데이터가 끝났다는 뜻
            {
                rankDatas.Add(JsonUtility.FromJson<RankSet>(slicedData));
                slicedData = "";
                i++;                        // '}'뒤에는 ','가 있으니 1을 더해준다
                
                /*
                 for문 조건식 i-1한 이유 :
                 마지막요소는 '}'뒤에 ','가 없기 때문에
                 ']'를 건너 뜀
                */
            }
        }

        for (int i = 0; i < maxRanks; i++)
        {
            TimeSpan t = TimeSpan.FromSeconds(rankDatas[i].time);
            ranksTexts[i].text = $"Rank {i+1}. {rankDatas[i].playerName}\ntime : {t.Hours:D2}:{t.Minutes:D2}:{t.Seconds:D2}";
        }
    }
}
