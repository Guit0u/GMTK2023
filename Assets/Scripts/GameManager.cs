using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private DialogueReader reader;

    private bool timerIsRunning;
    private float timeRemaining;

    private void Awake()
    {
        reader.gameObject.SetActive(false);
    }

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

                SceneManager.LoadScene("Tribunal");
            }
        }
    }

    public void StartDialogue()
    {
        reader.gameObject.SetActive(true);
    }
}
