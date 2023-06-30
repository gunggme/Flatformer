using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Interactable
{
    public int sceneNum;

    protected override void Interact()
    {
        Debug.Log("Portal use!");
        SceneManager.LoadScene(sceneNum);
    }
}
