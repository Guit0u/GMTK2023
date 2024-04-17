using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Phone : MonoBehaviour
{
    [SerializeField] private MiniGame miniGame;
    [SerializeField] private Image image;
    [SerializeField] private Sprite newSprite;
    [SerializeField] private float delay;

    public void IsGood()
    {
        StartCoroutine(EndMiniGame());
    }

    private IEnumerator EndMiniGame()
    {
        miniGame.DisplayReturnButton(false);
        image.sprite = newSprite;

        yield return new WaitForSeconds(delay);

        miniGame.Win();
    }
}
