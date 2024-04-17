using System.Collections;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    [SerializeField] private MiniGame miniGame;
    [Header("Parameters")]
    [SerializeField] int numberOfClicks = 1;
    [SerializeField] float finalClickDelay = 1f;
    [SerializeField] Vector3 clickOffset;
    [SerializeField] Vector3 finalOffset;
    [SerializeField] Sprite newSprite;

    int currentClicks = 0;
    
    private void OnMouseDown()
    {
        currentClicks++;

        if (currentClicks == numberOfClicks)
        {
            StartCoroutine(FinalClick());
        }
        else if (currentClicks < numberOfClicks) 
        {
            gameObject.transform.position += clickOffset;
        }
        
    }

    private IEnumerator FinalClick()
    {
        miniGame.DisplayReturnButton(false);

        gameObject.transform.position += finalOffset;
        gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
        
        yield return new WaitForSeconds(finalClickDelay);

        miniGame.Win();
    }
}
