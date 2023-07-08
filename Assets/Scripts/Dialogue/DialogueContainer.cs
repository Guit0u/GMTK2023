using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueContainer : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField, Range(1, 10)] private int textSpeed = 3;
    
    private float waitBetweenLetter;
    private bool typing;

    [Header("Portrait")]
    [SerializeField] private List<Image> Portraits;

    [Header("Dialogue")]
    [SerializeField] private TextMeshProUGUI dialogue;
    [SerializeField] private TextMeshProUGUI author;
    [SerializeField] private GameObject arrow;

    [Header("Choices")]
    [SerializeField] private List<Button> buttons;

    public bool IsTyping { get => typing; }

    private void Start()
    {
        waitBetweenLetter = 0.1f / textSpeed;
    }

    public void UpdateValues(TextEntry entry, int textIndex)
    {
        ShowPortrait(entry.portrait);

        if (entry.type == EntryType.Text)
        {
            StartCoroutine(TypeText(entry.author, entry.text[textIndex]));
        }
        else
        {
            SetChoice(entry.author, entry.text);
        }
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

    public void ResetText()
    {
        foreach (Button button in buttons) { button.gameObject.SetActive(false); }

        author.text = string.Empty;
        dialogue.text = string.Empty;

        arrow.SetActive(false);
    }

    public void SetText(TextEntry entry, int entryIndex)
    {
        StopAllCoroutines();

        foreach (Button button in buttons) { button.gameObject.SetActive(false); }

        author.text = entry.author;
        dialogue.text = entry.text[entryIndex];

        arrow.SetActive(true);
        typing = false;
    }

    private void SetChoice(string authorName, List<string> choices)
    {
        author.text = authorName;
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

        author.text = authorName;

        foreach (char letter in text.ToCharArray())
        {
            waitBetweenLetter = 0.1f / textSpeed;

            dialogue.text += letter;
            yield return new WaitForSeconds(waitBetweenLetter);
        }

        arrow.SetActive(true);
        typing = false;
    }
}
