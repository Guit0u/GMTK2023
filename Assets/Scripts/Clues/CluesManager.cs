using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CluesManager : MonoBehaviour
{
    //public List<Clue> cluesFound = new List<Clue>();
    public Dictionary<Image, Clue> cluesFound = new Dictionary<Image, Clue>();

    public void FoundClue(Clue clue)
    {
        Image newImage = clue.UIClue.GetComponent<UI_Clue>().ClueImage;
        cluesFound.Add(newImage, clue);

    }

    public void DisplayClues()
    {
        foreach(Clue clue in cluesFound)
        {

            //clue.UIClue.GetComponent<UI_Clue>().ClueImage;
        }
    }
}
