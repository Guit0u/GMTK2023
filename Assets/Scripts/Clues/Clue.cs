using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clue : MonoBehaviour
{
    [SerializeField] private ClueData clueData;
    [SerializeField] private string miniGame;

    [SerializeField] private GameObject UiCluePrefab;

    private bool miniGameDone;

    public void MiniGameDone() => miniGameDone = true;

    private void OpenUI()
    {
        GameObject uiClue = Instantiate(UiCluePrefab, transform);
        uiClue.GetComponent<UI_Clue>().Setup(this, clueData, miniGame, miniGameDone);
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        clueData.clueState = CluesManager.Instance.GetClueState(clueData.clueName);

        if (clueData.clueState == ClueState.NotFound)
        {
            clueData.clueState = ClueState.Found;
            CluesManager.Instance.FoundClue(clueData);
        }

        OpenUI();
    }
}
