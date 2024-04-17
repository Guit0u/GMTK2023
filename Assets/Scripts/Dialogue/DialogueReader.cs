using System.Collections.Generic;
using UnityEngine;

public class DialogueReader : MonoBehaviour
{
    [SerializeField] private DialogueBranch dialogue;
    [SerializeField] private DialogueContainer dialogueContainer;

    private int entryIndex = 0;
    private int entryTextIndex = 0;

    public void Reset()
    {
        entryIndex = 0;
        entryTextIndex = 0;
        
        dialogueContainer.ResetText();
    }

    public void SetDialogue(DialogueBranch dialogue) => this.dialogue = dialogue;

    public void Next()
    {
        TextEntry currentEntry = dialogue.entries[entryIndex];

        if (currentEntry.type == EntryType.Choice) return;

        if (dialogueContainer.IsTyping)
        {
            dialogueContainer.SkipText();
            return;
        }

        entryTextIndex++;

        if (entryTextIndex < dialogue.entries[entryIndex].text.Count)
        {
            UpdateContainer();
        }
        else
        {
            entryTextIndex = 0;
            entryIndex++;
            if (entryIndex < dialogue.entries.Count)
            {
                UpdateContainer();
            }
            else 
            {
                NextBranch();
            }
        }
    }

    private void NextBranch()
    {
        if (dialogue.following.Count == 0)
        {
            GameManager.Instance.NextChapter();
            return;
        }
        int index = 0;
        while (index < dialogue.following.Count)
        {
            if (dialogue.following[index] != null &&
                Evaluate(dialogue.following[index].condition))
            {
                dialogue = dialogue.following[index];
                entryIndex = 0;
                entryTextIndex = 0;

                SusManager.Instance.UpdateSusValues(dialogue.SusModifier, dialogue.SusMarieModifier, dialogue.SusRobinModifier);
                UpdateContainer();
                return;
            }
            
            index++;
        }

        GameManager.Instance.NextChapter();
    }

    public void Choose(int index)
    {
        if (dialogue.following.Count > index && dialogue.following[index] != null)
        {
            dialogue = dialogue.following[index];
            entryIndex = 0;
            entryTextIndex = 0;

            SusManager.Instance.UpdateSusValues(dialogue.SusModifier, dialogue.SusMarieModifier, dialogue.SusRobinModifier);
            UpdateContainer();
        }
    }

    public void UpdateContainer()
    {
        TextEntry currentEntry = dialogue.entries[entryIndex];

        if (currentEntry.type == EntryType.Text)
        {
            dialogueContainer.StartTyping(currentEntry, entryTextIndex);
        }
        else if (currentEntry.type == EntryType.Choice)
        {
            List<string> choices = new();
            List<int> removeIndexes = new();

            for (int index = 0; index < dialogue.following.Count; index++ )
            {
                if (Evaluate(dialogue.following[index].condition))
                {
                    choices.Add(currentEntry.text[index]);
                }
                else removeIndexes.Add(index);
            }

            //Remove inaccessible following entries
            for(int i = removeIndexes.Count - 1; i >= 0; i--)
            {
                dialogue.following.RemoveAt(i);
            }

            dialogueContainer.ShowPortrait(currentEntry.portrait);
            dialogueContainer.ShowChoices(currentEntry.author, choices);
        }
    }

    public bool Evaluate(DialogueCondition condition)
    {
        if (condition.susCondition != SusCondition.None)
        {
            int suspicion = SusManager.Instance.Suspicion;

            if (condition.susCondition == SusCondition.Below && suspicion > condition.susValue) return false;
            if (condition.susCondition == SusCondition.Above && suspicion < condition.susValue) return false;
        }

        foreach (ClueCondition clueCondition in condition.clues)
        {
            if (CluesManager.Instance.CluesData.TryGetValue(clueCondition.name, out ClueData data))
            {
                if (clueCondition.IsNot ^ data.clueState == clueCondition.state) 
                    return false;
            }
        }

        return true;
    }
}
