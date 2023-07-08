using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueBranch", fileName = "DialogueBranch")]
public class DialogueBranch : ScriptableObject
{
    public List<TextEntry> entries = new();
}
