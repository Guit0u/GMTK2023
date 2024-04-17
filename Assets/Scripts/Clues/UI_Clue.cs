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

    private ClueData clueData;
    private string miniGameName;

    private bool miniGameDone;

    public void Setup(ClueData data, string miniGame, bool miniGameDone)
    { 
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
                miniGameDone = true;
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
            CluesManager.Instance.StartClueMiniGame(miniGameName);
            SceneManager.LoadScene(miniGameName, LoadSceneMode.Additive);

            Destroy(gameObject);
        }
    }
}
