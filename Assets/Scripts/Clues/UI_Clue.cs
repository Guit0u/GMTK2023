using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Clue : MonoBehaviour
{
    [SerializeField] private GameObject MiniGameButton;
    [SerializeField] private Image ClueImage;

    [SerializeField] private TMP_Text textName;
    [SerializeField] private TMP_Text textDescription;

    private Clue clue;
    private ClueData clueData;
    private string miniGameName;

    private bool miniGameDone;

    public void Setup(Clue clue, ClueData data, string miniGame, bool miniGameDone)
    { 
        this.clue = clue;
        clueData = data;
        miniGameName = miniGame;
        this.miniGameDone = miniGameDone;

        textName.text = clueData.clueName;

        switch(clueData.clueState)
        {
            case ClueState.NotFound:
                textDescription.text = clueData.description;
                ClueImage.sprite = clueData.image;
                break;

            case ClueState.Found:
                textDescription.text = clueData.description; 
                ClueImage.sprite = clueData.image;
                break;

            case ClueState.Affected:
                textDescription.text = clueData.affectedDescription;
                ClueImage.sprite = clueData.affectedImage;
                break;
        }

        if (miniGameDone || miniGameName == string.Empty) MiniGameButton.SetActive(false);
    }

    public void OkButtonClick()
    {
        Destroy(gameObject);
    }

    public void MiniGameButtonClick()
    {
        if (!miniGameDone)
        {
            miniGameDone = true;
            clue.MiniGameDone();

            SceneManager.LoadScene(miniGameName, LoadSceneMode.Additive);

            Destroy(gameObject);
        }
    }
}
