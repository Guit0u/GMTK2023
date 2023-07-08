 using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Clue : MonoBehaviour
{
    public Clue clue;
    [SerializeField] Button MiniGameButton;
    [SerializeField] public Image ClueImage;
    [SerializeField] public TMP_Text textDescription;
    [SerializeField] public TMP_Text textName;
    bool hasAlreadyDoTheMiniGame;

    private void Start()
    { 
        if (clue.miniGame=="")
        {
            MiniGameButton.gameObject.SetActive(false);
        }
    }

    public void OkButtonClick()
    {
        gameObject.SetActive(false);
    }

    public void MiniGameButtonClick()
    {
        if (!hasAlreadyDoTheMiniGame) {
        hasAlreadyDoTheMiniGame = true;
        SceneManager.LoadSceneAsync(clue.miniGame, LoadSceneMode.Additive); }
        

    }
}
