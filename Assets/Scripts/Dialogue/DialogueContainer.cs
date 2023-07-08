using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueContainer : MonoBehaviour
{
    [Header("Portrait")]
    [SerializeField] private List<Image> Portraits;

    [Header("Dialogue")]
    [SerializeField] private TextMeshProUGUI dialogue;
    [SerializeField] private TextMeshProUGUI author;
    [SerializeField] private GameObject arrow;

    [Header("Choices")]
    [SerializeField] private List<Button> buttons;

    public void ShowPortrait(Portrait portrait)
    {
        foreach(Image image in Portraits) image.enabled = false;
        int imageIndex = (int) portrait.position;

        Portraits[imageIndex].sprite = portrait.sprite;
        Portraits[imageIndex].enabled = true;

        if (portrait.reversed) Portraits[imageIndex].gameObject.GetComponent<RectTransform>().localScale = new Vector3(-1f, 1f, 1f);
        else Portraits[imageIndex].gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
    }

    public void SetText(string authorName, string text)
    {
        foreach (Button button in buttons) { button.gameObject.SetActive(false); }

        author.text = authorName;
        dialogue.text = text;

        arrow.SetActive(true);
    }

    public void SetChoice(string authorName, List<string> choices)
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
}
