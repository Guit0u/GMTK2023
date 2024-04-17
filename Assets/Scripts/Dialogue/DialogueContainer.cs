using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueContainer : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField, Range(1, 10)] private int textSpeed = 3;
    [SerializeField, Range(0, 1)] private float shortPauseTime = 0.25f;
    [SerializeField, Range(0, 2)] private float longPauseTime = 1f;
    [SerializeField, Range(0, 1)] private float textBaseVolume = 0.2f;
    
    private float waitBetweenLetter;
    private bool typing;
    private bool skip;
    private Stack<TextTag> tagsOpen = new();

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
        dialogue.alignment = TextAlignmentOptions.TopLeft;

        arrow.SetActive(false);
    }

    public void StartTyping(TextEntry entry, int textIndex)
    {
        ShowPortrait(entry.portrait);
        StartCoroutine(TypeText(entry.author, entry.text[textIndex]));
    }

    public void SkipText()
    {
        skip = true;
    }

    //Unused, kept for debug (will cause text tags to be displayed on screen and disable their effect)
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

    public void ShowChoices(string authorName, List<string> choices)
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

        
        char[] chars = text.ToCharArray();

        for (int i = 0; i < chars.Length; i++)
        {
            if (CheckTag(chars, ref i, dialogue))
            {
                tagsOpen.TryPeek(out TextTag tag);
                switch (tag)
                {
                    case TextTag.SHORT_PAUSE:
                        audioSource.loop = false;
                        yield return new WaitForSeconds(skip ? 0 : shortPauseTime);
                        tagsOpen.Pop();
                        audioSource.loop = true;
                        audioSource.Play();
                        break;

                    case TextTag.LONG_PAUSE:
                        audioSource.loop = false;
                        yield return new WaitForSeconds(skip ? 0 : longPauseTime);
                        tagsOpen.Pop();
                        audioSource.loop = true;
                        audioSource.Play();
                        break;
                }
            }
            else
            {
                dialogue.text += chars[i];

                waitBetweenLetter = 0.1f / textSpeed;
                yield return new WaitForSeconds(skip ? 0 : waitBetweenLetter);
            }
        }

        audioSource.loop = false;
        arrow.SetActive(true);
        tagsOpen.Clear();
        typing = false;
        skip = false;
    }

    private bool CheckTag(char[] chars, ref int index, TextMeshProUGUI dialogue)
    {
        if (chars[index] == '<')
        {
            index++;
            char tagLetter = chars[index];
            tagsOpen.TryPeek(out TextTag lastTagOpen);

            switch (tagLetter)
            {
                case 'b':
                    if (lastTagOpen == TextTag.BOLD)
                    {
                        tagsOpen.Pop();
                        dialogue.text += @"</b>";
                    }
                    else
                    {
                        tagsOpen.Push(TextTag.BOLD);
                        dialogue.text += "<b>";
                    }
                    break;
                case 'i':
                    if (lastTagOpen == TextTag.ITALIC)
                    {
                        tagsOpen.Pop();
                        dialogue.text += @"</i>";
                    }
                    else
                    {
                        tagsOpen.Push(TextTag.ITALIC);
                        dialogue.text += "<i>";
                    }
                    break;
                case 'c':
                    index++;
                    if (lastTagOpen == TextTag.COLOR)
                    {
                        tagsOpen.Pop();
                        dialogue.text += @"</color>";
                    }
                    else if (chars[index] == '=')
                    {
                        tagsOpen.Push(TextTag.COLOR);
                        index++;
                        char colorLetter = chars[index];

                        switch (colorLetter)
                        {
                            case 'r':
                                dialogue.text += "<color=red>"; //Red
                                break;
                            case 'g':
                                dialogue.text += "<color=green>"; //Green
                                break;
                            case 'b':
                                dialogue.text += "<color=#0080FF>"; //Blue
                                break;
                        }
                    }
                    break;
                case 'm':
                    dialogue.alignment = TextAlignmentOptions.Center;
                    break;
                case '.':
                    tagsOpen.Push(TextTag.SHORT_PAUSE);
                    break;
                case '_':
                    tagsOpen.Push(TextTag.LONG_PAUSE);
                    break;

            }

            while (chars[index] != '>') index++;
            return true;
        }

        return false;
    }

    public void SetTextVolume(Single value)
    {
        audioSource.volume = value * textBaseVolume;
    }

    public void SetTextSpeed(Single value)
    {
        textSpeed = (int)value;
    }
}
