using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DraggedObject : MonoBehaviour
{
    Vector2 difference = Vector2.zero;
    bool isFollowing;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnMouseDown()
    {
        isFollowing=!(isFollowing);
        difference = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
        
    }

    private void Update()
    {
        if (isFollowing)
        {
            Vector2 move = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
            if(Vector2.Distance((Vector2)transform.position, move)<0.4)
                rb.MovePosition(move);

        }


    }

}
