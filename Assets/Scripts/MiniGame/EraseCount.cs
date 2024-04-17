using UnityEngine;

public class EraseCount : MonoBehaviour
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
