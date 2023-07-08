using System;
using System.Collections.Generic;

[Serializable]
public enum SusCondition
{
    None,
    Below,
    Above
}

[Serializable]
public class DialogueCondition
{
    public SusCondition susCondition;
    public int susValue;

    public Dictionary<string, ClueState> clues;
}
