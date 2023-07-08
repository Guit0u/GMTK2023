using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGame : MonoBehaviour
{
    public float timeRemaining = 20;
    public bool timerIsRunning = false;
    public TMP_Text timerText;
    public bool hasWin=false;
    public Clue relatedClue;
    public string miniGameName;
    CluesManager CM;


    private void Start()
    {
        timerIsRunning = true;
        CM = FindObjectOfType<CluesManager>();
        CM.HideAllUIClue();
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
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                SceneManager.UnloadSceneAsync(miniGameName);
                //SceneManager.LoadScene("PromScene");
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

    public void GetRelatedClue(){
        foreach (Clue clue in CM.Clues)
        {
            if (clue.miniGame == miniGameName)
            {
                relatedClue = clue;
                return;
            }
        }
    }

    public void Win()
    {
        hasWin = true;
        GetRelatedClue();
        relatedClue.clueState = ClueState.HasBeenAffected;
        SceneManager.UnloadSceneAsync(miniGameName);
    }


}
