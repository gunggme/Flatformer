using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameUISet : MonoBehaviour
{
    public TMP_Text nameText;

    private void Start()
    {
        nameText.text = GameManager.instance.saveManager.rank.playerName;
    }
}
