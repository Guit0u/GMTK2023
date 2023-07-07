 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Clue : MonoBehaviour
{
    public Clue clue;
    [SerializeField] Button MiniGameButton;
    public void OkButtonClick()
    {
        gameObject.SetActive(false);
        if (!clue.miniGame)
        {
            MiniGameButton.gameObject.SetActive(false);
        }
    }
}
