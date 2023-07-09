using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Phone : MonoBehaviour
{

    public void IsGood()
    {
        FindObjectOfType<MiniGame>().Win();
    }

    public void IsBad()
    {
        FindObjectOfType<MiniGame>().timeRemaining = 0;
    }
}
