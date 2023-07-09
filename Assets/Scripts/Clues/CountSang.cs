using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountSang : MonoBehaviour
{
    public TacheSang[] tachessang;
    bool win=false;

    private void Start()
    {
        tachessang = FindObjectsOfType<TacheSang>();
    }
    private void Update()
    {
        Winning();
        if (win)
        {
            FindObjectOfType<MiniGame>().Win();
        }
    }

    private void Winning()
    {
        foreach(TacheSang tache in tachessang)
        {
            if (!tache.isOk)
            {
                return;
            }
        }
        win = true;
    }

}
