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
        }
    }

    private void UpdateContainer()
    {
        TextEntry currentEntry = dialogue.entries[entryIndex];
        dialogueContainer.UpdateValues(currentEntry, entryTextIndex);
    }
}
