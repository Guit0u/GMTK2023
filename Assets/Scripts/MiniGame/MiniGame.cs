using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MiniGame : MonoBehaviour
{
    [SerializeField] private string relatedClue;
    [SerializeField] private GameObject returnButton;

    private void Start()
    {
        CluesManager.Instance.HideClues();
        BoxClue(false);
    }

    private void BoxClue(bool isEnabled)
    {
        Clue[] clues = FindObjectsOfType<Clue>();
        foreach (Clue clue in clues)
        {
            clue.GetComponent<BoxCollider2D>().enabled = isEnabled;
        }
    }

    public void DisplayReturnButton(bool active)
    {
        returnButton.SetActive(active);
    }

    public void ReturnToCrimeScene()
    {
        BoxClue(true);
        SceneManager.UnloadSceneAsync(CluesManager.Instance.EndClueMiniGame());
    }

    public void Win()
    {
        CluesManager.Instance.AffectClue(relatedClue);
        ReturnToCrimeScene();
    }
}
