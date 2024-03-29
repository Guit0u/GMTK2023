using System;
using TMPro;
using UnityEngine;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI musicVolumeText;
    [SerializeField] private TextMeshProUGUI sfxVolumeText;
    [SerializeField] private TextMeshProUGUI dialogueSpeedText;

    private readonly static int SLIDER_MAX = 100;

    public void UpdateMusicText(Single value)
    {
        musicVolumeText.text = Mathf.RoundToInt(value * SLIDER_MAX).ToString();
    }

    public void UpdateSFXText(Single value)
    {
        sfxVolumeText.text = Mathf.RoundToInt(value * SLIDER_MAX).ToString();
    }

    public void UpdateDialogueSpeedText(Single value)
    {
        dialogueSpeedText.text = Mathf.RoundToInt(value).ToString();
    }
}
