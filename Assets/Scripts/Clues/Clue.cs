using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public enum ClueState
{
    NotFound,HasBeenFound,HasBeenAffected
}
public class Clue : MonoBehaviour
{
    CluesManager CM;
    public GameObject UIClue;

    public string clueName;
    public ClueState clueState;
    public string description;

    [SerializeField] public string miniGame;

    private void Start()
    {
        CM = FindObjectOfType<CluesManager>();
    }

    protected void AddClue()
    {
        CM.FoundClue(this);
    }

    public void DisplayUI()
    {
        CM.HideAllUIClue();
        UIClue.gameObject.SetActive(true);
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (clueState== ClueState.NotFound)
        {
            SpawnUI();
            AddClue();
            clueState = ClueState.HasBeenFound;
        }
        else
        {
            DisplayUI();
        }
    }

    private void SpawnUI()
    {
        UIClue.GetComponent<UI_Clue>().clue = this;
        UIClue = Instantiate(UIClue, transform.position, Quaternion.identity);
        
    }

}
