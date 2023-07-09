using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float timer;

    private bool timerIsRunning;
    private float timeRemaining;

    void Start()
    {
        timeRemaining = timer; 
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;

                SceneManager.LoadScene(4);
            }
        }
    }
}
