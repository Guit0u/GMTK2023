using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clue : MonoBehaviour
{
    CluesManager CM;
    public GameObject UIClue;
    bool hasBeenFound = false;
    public string ClueName;
    public string state; //a definir
    [SerializeField] public MiniGame miniGame;

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
        if (!haveBeenFound)
        {
            SpawnUI();
            AddClue();
            haveBeenFound = true;
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
