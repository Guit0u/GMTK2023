using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueReader : MonoBehaviour
{
    [SerializeField] private DialogueBranch dialogue;
    [SerializeField] private DialogueContainer dialogueContainer;

    private int entryIndex = 0;
    private int entryTextIndex = 0;

    void Start()
    {
        UpdateContainer();
    }

    public void Next()
    {
        if (dialogueContainer.IsTyping)
        {
            TextEntry currentEntry = dialogue.entries[entryIndex];
            dialogueContainer.SetText(currentEntry, entryTextIndex);

            return;
        }

        entryTextIndex++;

        if (entryTextIndex < dialogue.entries[entryIndex].text.Count)
        {
            UpdateContainer();
        }
        else
        {
            entryIndex++;
            entryTextIndex = 0;

            if (entryIndex < dialogue.entries.Count) UpdateContainer();
            else Debug.Log("End of dialogue.");
        }
    }

    public void Choose(int index)
    {
        if (dialogue.following.Count > index && dialogue.following[index] != null)
        {
            dialogue = dialogue.following[index];

            entryIndex = 0;
            entryTextIndex = 0;
        }

        UpdateContainer();
    }

    private void UpdateContainer()
    {
        TextEntry currentEntry = dialogue.entries[entryIndex];
        dialogueContainer.UpdateValues(currentEntry, entryTextIndex);
    }
}
