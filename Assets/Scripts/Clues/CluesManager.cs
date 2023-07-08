using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CluesManager : MonoBehaviour
{
    public static CluesManager Instance;

    public Clue[] Clues;
    public Dictionary<Image, Clue> ImagesClue = new Dictionary<Image, Clue>();
    
    [SerializeField] Canvas canvas;
    bool isDisplaying = false;

    [SerializeField] Image[] images;
    [SerializeField] Image Background;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
        else Destroy(this);
    }

    private void Start()
    {
        Background.gameObject.SetActive(false);
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
        clue.DisplayUI();
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
            HideAllUIClue();
            Background.gameObject.SetActive(false);
            foreach (KeyValuePair<Image, Clue> clue in ImagesClue)
            {
                clue.Key.gameObject.SetActive(false);
            }
            isDisplaying = false;
        }
        else
        {
            HideAllUIClue();
            Background.gameObject.SetActive(true);
            foreach (KeyValuePair<Image, Clue> clue in ImagesClue)
            {
                if (clue.Value!= null)
                    clue.Key.gameObject.SetActive(true);
            }
            isDisplaying=true;
        }

    }

    public void HideMenu()
    {
        if (isDisplaying)
        {
            DisplayClues();
        }
    }

    public void HideAllUIClue()
    {
        foreach (KeyValuePair<Image, Clue> clue in ImagesClue)
        {
            if(clue.Value!=null && clue.Value.UIClue != null)
                clue.Value.UIClue.SetActive(false);
        }
    }
}
