using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InterractObject : MonoBehaviour
{
    private void OnMouseDown()
    {
        FindObjectOfType<MiniGame>().Win();
    }
}
