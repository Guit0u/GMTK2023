using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountSang : MonoBehaviour
{
    [SerializeField] private MiniGame miniGame;
    [SerializeField] private int bloodstainCount = 1;

    private int removed = 0;

    public void RemoveBloodstain()
    {
        removed++;
        if (removed >= bloodstainCount) miniGame.Win();
    }
}
