using System;
using UnityEngine;

[Serializable]
public enum ClueState
{
    NotFound,
    Found,
    Affected
}

[Serializable]
public class ClueData
{
    public string clueName;

    public ClueState clueState;

    public string description;
    public string affectedDescription;

    public Sprite image;
    public Sprite affectedImage;
}
