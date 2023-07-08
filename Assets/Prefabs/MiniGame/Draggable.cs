using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggedObject : MonoBehaviour
{
    Vector2 difference = Vector2.zero;
    bool isFollowing;
    private void OnMouseDown()
    {
        isFollowing=!isFollowing;
        difference = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
    }

    private void Update()
    {
        if(isFollowing)
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
    }

}
