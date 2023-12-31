using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueBranch", fileName = "DialogueBranch")]
public class DialogueBranch : ScriptableObject
{
    public DialogueCondition condition;

    public int SusModifier;
    public int SusMarieModifier;
    public int SusRobinModifier;
    
    [Space(10)]

    public List<TextEntry> entries = new();
    public List<DialogueBranch> following = new();
}
