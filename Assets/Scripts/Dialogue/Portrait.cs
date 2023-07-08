using System;
using UnityEngine;

[Serializable]
public enum PortraitPosition
{
    Left = 0,
    Middle = 1,
    Right = 2
}

[Serializable]
public class Portrait
{
    public Sprite sprite;
    public PortraitPosition position;
    public bool reversed;
}
