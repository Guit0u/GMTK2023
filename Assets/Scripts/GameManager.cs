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
    [SerializeField] private List<DialogueBranch> dialogues;

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
        reader.Reset();
        reader.gameObject.SetActive(false);

        timeRemaining = timer;

        if (chapter == Chapter.Intro)
        {
            StartDialogue();
            reader.SetDialogue(dialogues[0]);
        }
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
        reader.Reset();
        reader.gameObject.SetActive(false);

        if (chapter == Chapter.Intro)
        {
            SceneManager.LoadScene("CrimeScene");
<<<<<<< HEAD
            reader.SetDialogue(dialogues[1]);
=======
            timerIsRunning = true;
>>>>>>> 8aa0fa8dc0dc7e4adce5fdd37de031366a407114
        }
        else if (chapter == Chapter.Crime)
        {
            timerIsRunning = false;
            SceneManager.LoadScene("Tribunal");
            reader.SetDialogue(dialogues[2]);
        }
        else if (chapter == Chapter.Tribunal)
        {
            
            SceneManager.LoadScene("End");
            reader.SetDialogue(dialogues[3]);
        }
        else if (chapter == Chapter.End)
        {
            Application.Quit();
        }
    }
}
