using UnityEngine;
using UnityEngine.EventSystems;

public class Clue : MonoBehaviour
{
    [SerializeField] private ClueData clueData;
    [Space(10)]
    [SerializeField] private string miniGame;
    [SerializeField] private Sprite spriteAfterMiniGame;

    private bool miniGameDone;

    public void MiniGameDone()
    {
        GetComponent<SpriteRenderer>().sprite = spriteAfterMiniGame;
        miniGameDone = true;
    }

    private void OpenUI()
    {
        if (string.IsNullOrEmpty(miniGame)) MiniGameDone();
       
        GameObject uiClue = Instantiate(CluesManager.Instance.UICluePrefab, transform);
        uiClue.GetComponent<UI_Clue>().Setup(clueData, miniGame, miniGameDone);
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
            CluesManager.Instance.LinkClueObject(this, clueData.clueName);
        }

        OpenUI();
    }
}
