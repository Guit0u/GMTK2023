using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum EntryType
{
    Text,
    Choice
}

[Serializable]
public class TextEntry
{
    public EntryType type;
    public string author;

    public Portrait portrait;

    [TextArea(1, 3)]
    public List<string> text = new();
}
