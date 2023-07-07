using UnityEngine;

public class Post : MonoBehaviour
{
    private void Awake()
    {
        if (!GameManager.instance)
            return;
        GameManager.instance.saveManager.CallRankStructSave();
        StartCoroutine(GameManager.instance.saveManager.UnityWebRequestPOSTTEST("pcs.pah.kr:1005/api/insert"));
    }
}
