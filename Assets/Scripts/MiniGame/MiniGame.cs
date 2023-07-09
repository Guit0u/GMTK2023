using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGame : MonoBehaviour
{
    [SerializeField] private string miniGameName;
    [SerializeField] private string relatedClue;
    [Space(10)]
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private float timeRemaining = 20;
    
    private bool timerIsRunning = false;

    private void Start()
    {
        timerIsRunning = true;
        CluesManager.Instance.HideClues();
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;

                SceneManager.UnloadSceneAsync(miniGameName);
            }
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void Win()
    {
        CluesManager.Instance.AffectClue(relatedClue);
        SceneManager.UnloadSceneAsync(miniGameName);
    }
}
