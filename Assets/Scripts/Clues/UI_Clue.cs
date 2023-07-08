 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Clue : MonoBehaviour
{
    public Clue clue;
    [SerializeField] Button MiniGameButton;
    [SerializeField] public Image ClueImage;

    private void Start()
    { 
        if (clue.miniGame == null)
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
        SceneManager.LoadScene("MiniGameScene", LoadSceneMode.Additive);
        //SceneManager.UnloadSceneAsync("MiniGameScene");
    }
}
