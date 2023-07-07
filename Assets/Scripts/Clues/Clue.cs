using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Clue : MonoBehaviour
{
    CluesManager CM;
    public GameObject UIClue;
    bool haveBeenFound = false;
    [SerializeField] public MiniGame miniGame;

    private void Start()
    {
        CM = FindObjectOfType<CluesManager>();
    }

    protected void AddClue()
    {
        CM.FoundClue(this);
    }

    private void OnMouseDown()
    {
        print("Clue Found");
        if (!haveBeenFound)
        {
            SpawnUI();
            AddClue();
            haveBeenFound = true;
        }
        else
        {
            UIClue.SetActive(true);
        }
    }

    private void SpawnUI()
    {
        Instantiate(UIClue, transform.position, Quaternion.identity);
        UIClue.GetComponent<UI_Clue>().clue = this;
    }

}
