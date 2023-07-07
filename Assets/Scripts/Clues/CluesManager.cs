using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CluesManager : MonoBehaviour
{
    public List<Clue> CluesFound = new List<Clue>();

    public void FoundClue(Clue clue)
    {
        CluesFound.Add(clue);
    }


}
