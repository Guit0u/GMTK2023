using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CluesManager : MonoBehaviour
{
    public Clue[] Clues;
    [SerializeField] public Dictionary<Image, Clue> ImagesClue = new Dictionary<Image, Clue>();
    [SerializeField] Canvas canvas;
    bool isDisplaying = false;
    [SerializeField] Image[] images;

    private void Start()
    {
        Clues = GetComponents<Clue>();
        foreach(Image image in images)
        {
            ImagesClue.Add(image, null);
            image.gameObject.SetActive(false);
        }
    }

    public void FoundClue(Clue clue)
    {
        var ImageKey = ImagesClue.FirstOrDefault(p => p.Value is null).Key;
        ImagesClue[ImageKey] = clue;
        ImageKey.sprite = clue.UIClue.GetComponent<UI_Clue>().ClueImage.sprite;
        EventTrigger trigger = ImageKey.gameObject.AddComponent<EventTrigger>();
        AddEventTriggerListener(trigger, EventTriggerType.PointerClick, OnImageClicked);
    }

    void OnImageClicked(BaseEventData eventData)
    {
        PointerEventData pointerEventData = (PointerEventData)eventData;
        GameObject clickedImage = pointerEventData.pointerPress;
        Clue clue = ImagesClue[clickedImage.GetComponent<Image>()];
        clue.UIClue.SetActive(true);
    }

    public static void AddEventTriggerListener(EventTrigger trigger,
                                           EventTriggerType eventType,
                                           System.Action<BaseEventData> callback)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventType;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(new UnityEngine.Events.UnityAction<BaseEventData>(callback));
        trigger.triggers.Add(entry);
    }

    public void DisplayClues()
    {
        if (isDisplaying)
        {
            foreach (KeyValuePair<Image, Clue> clue in ImagesClue)
            {
                clue.Key.gameObject.SetActive(false);
            }
            isDisplaying = false;
        }
        else
        {
            foreach (KeyValuePair<Image, Clue> clue in ImagesClue)
            {
                if (clue.Value!= null)
                    clue.Key.gameObject.SetActive(true);
            }
            isDisplaying=true;
        }

    }
}
