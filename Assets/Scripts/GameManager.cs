using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum Chapter 
{
    Intro,Crime,Tribunal,End
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private float timer;
    [SerializeField] private DialogueReader reader;

    [SerializeField] private Chapter chapter;

    private Dictionary<Choice, bool> choices;

    private bool timerIsRunning;
    private float timeRemaining;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        reader.gameObject.SetActive(false);
    }

    void Start()
    {
        timeRemaining = timer; 
        //timerIsRunning = true;
        if (chapter == Chapter.Intro)
        {
            Intro();
        }
    }

    void Intro()
    {
        StartDialogue();
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

                NextChapter();
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

    public void NextChapter()
    {
        if (chapter == Chapter.Intro)
        {
            SceneManager.LoadScene("CrimeScene");
        }
        else if (chapter == Chapter.Crime)
        {
            SceneManager.LoadScene("Tribunal");
        }
        else if (chapter == Chapter.Tribunal)
        {
            SceneManager.LoadScene("End");
        }
        else if (chapter == Chapter.End)
        {
            Application.Quit();
        }
    }
}
