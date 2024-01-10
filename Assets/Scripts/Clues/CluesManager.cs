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
    [Header("Dependancies")]
    [SerializeField] private GameObject button;
    [SerializeField] private Image background;
    [SerializeField] private GameObject imagesContainer;

    public Dictionary<string, ClueData> Clues = new();
    private readonly Dictionary<Image, string> ImagesClue = new();

    private List<Image> images;

    private bool isClueMenuOpen = false;
    private bool inMiniGame = false;
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
        images = imagesContainer.GetComponentsInChildren<Image>().ToList();

        foreach(Image image in images)
        {
            ImagesClue.Add(image, null);
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

    public void StartClueMiniGame(string name)
    {
        inMiniGame = true;
        miniGameSceneName = name;
    }

    public string EndClueMiniGame()
    {
        inMiniGame = false;
        return miniGameSceneName;
    }
}
