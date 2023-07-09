using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.EventSystems;

public class InterractObject : MonoBehaviour
{

    [SerializeField] int numberOfClick = 1;
    int currentClick = 0;
    [SerializeField] Sprite newSprite;
    private void OnMouseDown()
    {
        currentClick++;
        if (currentClick == numberOfClick)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
            FindObjectOfType<MiniGame>().Win();
        }
        else
        {
            gameObject.transform.position += new Vector3(1, 1,0);
        }
        
    }
}
