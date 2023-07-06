using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }

    public void MoveMain()
    {
        rankImage.SetActive(false);
        main2Image.SetActive(true);
    }
}
