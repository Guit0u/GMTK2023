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
            else NextBranch();
        }
    }

    private void NextBranch()
    {
        if (dialogue.following == null) GameManager.Instance.NextChapter();
        else
        {
            int index = 0;
            while (!Evaluate(dialogue.following[index].condition))
            {
                index++;
            }
        }
    }

    public void Choose(int index)
    {
        if (dialogue.following.Count > index && dialogue.following[index] != null)
        {
            dialogue = dialogue.following[index];
            entryIndex = 0;
            entryTextIndex = 0;

            if (!Evaluate(dialogue.condition)) Choose(0);

            SusManager.Instance.ChangeSus(dialogue.SusModifier);
            UpdateContainer();
        }
    }

    private void UpdateContainer()
    {
        TextEntry currentEntry = dialogue.entries[entryIndex];
        dialogueContainer.UpdateValues(currentEntry, entryTextIndex);
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
            if (CluesManager.Instance.Clues.TryGetValue(clueCondition.name, out ClueData data))
            {
                if (data.clueState != clueCondition.state) return false;
            }
        }

        return true;
    }
}
