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
public class ClueCondition
{
    public string name;
    public ClueState state;
}

[Serializable]
public class DialogueCondition
{
    public SusCondition susCondition;
    public int susValue;

    public List<ClueCondition> clues;
}
