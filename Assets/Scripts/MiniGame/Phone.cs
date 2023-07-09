using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Phone : MonoBehaviour
{
    BoxCollider2D[] boxs;

    private void Start()
    {
        boxs=GetComponentsInChildren<BoxCollider2D>();
    }

    private void Update()
    {
        //print(boxs[1]);
        //print(boxs[1].tag);
    }

    private void OnMouseDown()
    {
        print("oooooooooooooooooo");
    }
}
