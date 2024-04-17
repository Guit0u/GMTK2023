using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

enum Chapter 
{
    Intro,Crime,CrimeExplore,Tribunal,End,GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Gameplay Parameters")]
    [SerializeField] private int suspicionGameOverValue;
    [SerializeField] private float timeToExploreCrimeScene;
    [Header("Dependancies")]
    [SerializeField] private DialogueReader reader;
    [SerializeField] private GameObject timer;
    [Space(10)]
    [SerializeField] private List<DialogueBranch> dialogues;
    [Header("Debug")]
    [SerializeField] private Chapter chapter;

    private readonly Dictionary<Choice, bool> choices;

    private bool timerIsRunning = false;
    private float timeRemaining;
    private Slider timerSlider;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        reader.gameObject.SetActive(false);
        timerSlider = timer.GetComponentInChildren<Slider>();
        timerSlider.maxValue = timeToExploreCrimeScene;
        timerSlider.value = timeToExploreCrimeScene;
        timer.SetActive(false);
    }

    void Start()
    {
        reader.Reset();
        reader.gameObject.SetActive(false);

        if (chapter == Chapter.Intro)
        {
            SusManager.Instance.ShowSlider(false);
            CluesManager.Instance.ShowClueButton(false);

            reader.SetDialogue(dialogues[0]);
            StartDialogue();
        }
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                timerSlider.value = timeRemaining;
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;

                NextChapter();
            }
        }

        if (SusManager.Instance.Suspicion > suspicionGameOverValue)
        {
            chapter = Chapter.GameOver;
            reader.SetDialogue(dialogues[3]);
            StartDialogue();
            SusManager.Instance.Suspicion = 0;
        }
    }

    public void StartDialogue()
    {
        reader.gameObject.SetActive(true);
        reader.UpdateContainer();
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
            chapter = Chapter.Crime;

            reader.SetDialogue(dialogues[1]);
            StartDialogue();
        }
        else if (chapter == Chapter.Crime)
        {
            chapter = Chapter.CrimeExplore;

            timeRemaining = timeToExploreCrimeScene;
            timer.SetActive(true);
            timerIsRunning = true;

            CluesManager.Instance.ShowClueButton(true);
        }
        else if (chapter == Chapter.CrimeExplore)
        {
            timer.SetActive(false);
            timerIsRunning = false;
            SceneManager.LoadScene("Tribunal");
            chapter = Chapter.Tribunal;

            reader.SetDialogue(dialogues[2]);
            StartDialogue();

            SusManager.Instance.ShowSlider(true);
        }
        else if (chapter == Chapter.Tribunal)
        {
            Debug.Log("Not Guilty end!");
            Application.Quit();
        }
        else if (chapter == Chapter.GameOver)
        {
            Debug.Log("Guilty gameover");
            Application.Quit();
        }
    }
}
