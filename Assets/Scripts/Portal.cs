using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Interactable
{
    public string sceneName;

    [SerializeField] private BlackBar blackBar;
    
    protected override void Interact()
    {
        Debug.Log("Portal use!");
        blackBar.CloseAnim();
    }

    public void ChangeScene()
    {
        if (sceneName == "GameOver")
            GameManager.instance.SaveDataToJson();
        SceneManager.LoadScene(sceneName);
    }
}
