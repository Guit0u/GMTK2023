using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueContainer : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField, Range(1, 10)] private int textSpeed = 3;
    [SerializeField, Range(0, 1)] private float textBaseVolume = 0.2f;
    
    private float waitBetweenLetter;
    private bool typing;

    [Header("Portrait")]
    [SerializeField] private List<Image> Portraits;
    [SerializeField] private Image author;

    [Header("Dialogue")]
    [SerializeField] private TextMeshProUGUI dialogue;
    [SerializeField] private TextMeshProUGUI authorName;
    [SerializeField] private GameObject arrow;

    [Header("Choices")]
    [SerializeField] private List<Button> buttons;

    public bool IsTyping { get => typing; }

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        waitBetweenLetter = 0.1f / textSpeed;
        audioSource.volume = textBaseVolume;
    }

    public void ShowPortrait(Portrait portrait)
    {
        foreach(Image image in Portraits) image.enabled = false;
        int imageIndex = (int) portrait.position;

        Portraits[imageIndex].sprite = portrait.sprite;
        Portraits[imageIndex].enabled = true;

        if (portrait.reversed) Portraits[imageIndex].gameObject.GetComponent<RectTransform>().localScale = new Vector3(-1f, 1f, 1f);
        else Portraits[imageIndex].gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
    }

    private void SetAuthor(string name)
    {
        author.enabled = name != string.Empty;
        authorName.text = name;
    }

    public void ResetText()
    {
        foreach (Button button in buttons) { button.gameObject.SetActive(false); }

        authorName.text = string.Empty;
        dialogue.text = string.Empty;

        arrow.SetActive(false);
    }

    public void StartTyping(TextEntry entry, int textIndex)
    {
        ShowPortrait(entry.portrait);
        StartCoroutine(TypeText(entry.author, entry.text[textIndex]));
    }

    public void SetText(TextEntry entry, int entryIndex)
    {
        StopAllCoroutines();

        foreach (Button button in buttons) { button.gameObject.SetActive(false); }

        SetAuthor(entry.author);
        dialogue.text = entry.text[entryIndex];

        audioSource.loop = false;
        arrow.SetActive(true);
        typing = false;
    }

    public void SetChoice(string authorName, List<string> choices)
    {
        SetAuthor(authorName);
        dialogue.text = string.Empty;

        arrow.SetActive(false);

        for(int i = 0; i < choices.Count; i++)
        {
            buttons[i].gameObject.SetActive(true);
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = choices[i];
        }
    }

    private IEnumerator TypeText(string authorName, string text)
    {
        ResetText();
        typing = true;
        SetAuthor(authorName);

        audioSource.Play();
        audioSource.loop = true;

        foreach (char letter in text.ToCharArray())
        {
            waitBetweenLetter = 0.1f / textSpeed;

            dialogue.text += letter;
            yield return new WaitForSeconds(waitBetweenLetter);
        }

        audioSource.loop = false;
        arrow.SetActive(true);
        typing = false;
    }
}
