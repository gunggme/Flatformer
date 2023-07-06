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
        StartCoroutine(SetRank());
    }

    public void MoveMain()
    {
        rankImage.SetActive(false);
        main2Image.SetActive(true);
    }


    private IEnumerator SetRank()
    {
        UnityWebRequest request = UnityWebRequest.Get("pcs.pah.kr:1005/api/ranking");
        yield return request.SendWebRequest();

        string rawData = request.error == null ? request.downloadHandler.text : "error";

        List<RankSet> rankDatas = new();
        string slicedData = "";
        // 쓸데없는 정보들 다 버리고 content 안에 있는 내용만 추출
        for (int i = 12; rawData[i-1] != ']' && rankDatas.Count < 4; i++) // 마지막 요소이거나, 상위 4명을 다 뽑았을 때 break
        {
            slicedData += rawData[i];
            if (rawData[i] == '}') // '}'이 나왔다는 것은 데이터가 끝났다는 뜻
            {
                rankDatas.Add(JsonUtility.FromJson<RankSet>(slicedData));
                slicedData = "";
                i++; // '}'뒤에는 ','가 있으니 1을 더해준다
                
                /*
                 for문 조건식 i-1한 이유
                 마지막에 i++이 실행되면 ']'을 건너뛰기 때문에
                */
            }
        }

        for (int i = 0; i < 4; i++) // 상위 4위까지
        {
            TimeSpan t = TimeSpan.FromSeconds(rankDatas[i].time);
            ranksTexts[i].text = $"Rank {i+1}. {rankDatas[i].playerName}\ntime : {t.Hours:D2}:{t.Minutes:D2}:{t.Seconds:D2}";
        }
    }
}
