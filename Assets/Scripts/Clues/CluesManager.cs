using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CluesManager : MonoBehaviour
{
    public static CluesManager Instance;

    [SerializeField] private GameObject uiCluePrefab;
    [Header("Dependancies")]
    [SerializeField] private GameObject button;
    [SerializeField] private Image background;
    [SerializeField] private GameObject imagesContainer;
    [SerializeField] private GameObject clueObjectsContainer;

    public GameObject UICluePrefab => uiCluePrefab;
    public Dictionary<string, ClueData> CluesData = new();

    private readonly Dictionary<string, Clue> Clues = new();
    private readonly Dictionary<Image, string> clueImages = new();
    private List<Image> menuImages;

    private bool isClueMenuOpen = false;
    private string miniGameSceneName;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        background.gameObject.SetActive(false);
        menuImages = imagesContainer.GetComponentsInChildren<Image>().ToList();

        foreach(Image image in menuImages)
        {
            clueImages.Add(image, null);
        }
    }

    public ClueState GetClueState(string clueName)
    {
        if (CluesData.TryGetValue(clueName, out ClueData clueData))
        {
            return clueData.clueState;
        }
        else return ClueState.NotFound;
    }

    public void FoundClue(ClueData clueData)
    {
        if (CluesData.ContainsKey(clueData.clueName)) return;

        CluesData.Add(clueData.clueName, clueData);

        var ImageKey = clueImages.FirstOrDefault(p => p.Value is null).Key;
        
        clueImages[ImageKey] = clueData.clueName;
        ImageKey.sprite = clueData.image;
        
        EventTrigger trigger = ImageKey.gameObject.AddComponent<EventTrigger>();
        AddEventTriggerListener(trigger, EventTriggerType.PointerClick, OnImageClicked);
    }

    public void LinkClueObject(Clue clue, string name)
    {
        if (Clues.ContainsKey(name)) return;
        else Clues.Add(name, clue);
    }

    public void AffectClue(string name)
    {
        CluesData.GetValueOrDefault(name).clueState = ClueState.Affected;
        Clues.GetValueOrDefault(name).MiniGameDone();
        Debug.Log(name + " was affected");
    }

    public void DisplayClues()
    {
        isClueMenuOpen = !isClueMenuOpen;
        background.gameObject.SetActive(isClueMenuOpen);
    }

    public void HideClues()
    {
        isClueMenuOpen = false;
        background.gameObject.SetActive(false);
    }

    public void ShowClueButton(bool active)
    {
        button.SetActive(active);
    }

    void OnImageClicked(BaseEventData eventData)
    {
        PointerEventData pointerEventData = (PointerEventData)eventData;
        GameObject clickedImage = pointerEventData.pointerPress;

        ClueData clueData = CluesData.GetValueOrDefault(clueImages[clickedImage.GetComponent<Image>()]);
        GameObject uiClue = Instantiate(uiCluePrefab);

        uiClue.GetComponent<UI_Clue>().Setup(clueData, string.Empty, true);
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

    public void StartClueMiniGame(string name)
    {
        miniGameSceneName = name;
        button.SetActive(false);
    }

    public string EndClueMiniGame()
    {
        button.SetActive(true);
        return miniGameSceneName;
    }
}
