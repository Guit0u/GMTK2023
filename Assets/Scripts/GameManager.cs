using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private DialogueReader reader;

    private Dictionary<Choice, bool> choices;

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

    public void SetChoice(Choice choice, bool state)
    {
        if (choices.ContainsKey(choice))
        {
            choices[choice] = state;
        }
        else choices.Add(choice, state);
    }

    public static void NextChapter()
    {

    }
}
