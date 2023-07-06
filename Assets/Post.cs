using UnityEngine;

public class Post : MonoBehaviour
{
    private void Awake()
    {
        GameManager.instance.saveManager.CallRankStructSave();
    }

    private void Start()
    {
        StartCoroutine(GameManager.instance.saveManager.UnityWebRequestPOSTTEST("pcs.pah.kr:1005/api/insert"));
    }
}
