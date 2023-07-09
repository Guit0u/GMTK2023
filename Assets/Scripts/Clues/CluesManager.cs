using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CluesManager : MonoBehaviour
{
    public static CluesManager Instance;

    [SerializeField] private GameObject uiCluePrefab;
    [Space(10)]

    [SerializeField] private Image background;
    [SerializeField] private List<Image> images;

    public Dictionary<string, ClueData> Clues = new();
    private readonly Dictionary<Image, string> ImagesClue = new();

    private bool isDisplaying;

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
        background.gameObject.SetActive(false);

        foreach(Image image in images)
        {
            ImagesClue.Add(image, null);
            image.gameObject.SetActive(false);
        }
    }

    public ClueState GetClueState(string clueName)
    {
        if (Clues.TryGetValue(clueName, out ClueData clueData))
        {
            return clueData.clueState;
            
        }
        else return ClueState.NotFound;
    }

    public void FoundClue(ClueData clueData)
    {
        if (Clues.ContainsKey(clueData.clueName)) return;

        Clues.Add(clueData.clueName, clueData);

        var ImageKey = ImagesClue.FirstOrDefault(p => p.Value is null).Key;
        
        ImagesClue[ImageKey] = clueData.clueName;
        ImageKey.sprite = clueData.image;
        
        EventTrigger trigger = ImageKey.gameObject.AddComponent<EventTrigger>();
        AddEventTriggerListener(trigger, EventTriggerType.PointerClick, OnImageClicked);
    }

    public void AffectClue(string name)
    {
        Clues.GetValueOrDefault(name).clueState = ClueState.Affected;
        Debug.Log(name + " was affected");
    }

    public void DisplayClues()
    {
        isDisplaying = !isDisplaying;
        background.gameObject.SetActive(isDisplaying);

        if (!isDisplaying) return;

        foreach (Image image in ImagesClue.Keys)
        {
            if (ImagesClue[image] != null)
            {
                image.gameObject.SetActive(true);
            }
            else
            {
                image.gameObject.SetActive(false);
            }
        }
    }

    public void HideClues()
    {
        isDisplaying = false;
        background.gameObject.SetActive(false);
    }

    void OnImageClicked(BaseEventData eventData)
    {
        PointerEventData pointerEventData = (PointerEventData)eventData;
        GameObject clickedImage = pointerEventData.pointerPress;

        ClueData clueData = Clues.GetValueOrDefault(ImagesClue[clickedImage.GetComponent<Image>()]);
        GameObject uiClue = Instantiate(uiCluePrefab);

        uiClue.GetComponent<UI_Clue>().Setup(null, clueData, string.Empty, true);
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
}
