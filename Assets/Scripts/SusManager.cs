using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SusManager : MonoBehaviour
{
    public static SusManager Instance;

    [SerializeField] private Slider susMeter;
    [SerializeField] private Image susFill;
    [SerializeField] private Image susOverfill;
    [SerializeField] private Gradient gradient;

    [Header("Parameters")]
    [SerializeField, Range(0, 1)] private float changeSpeed = 0.5f;

    [Header("Debug")]
    [SerializeField] private int suspicion = 0;
    [SerializeField] private int suspicionMary = 0;
    [SerializeField] private int suspicionRobin = 0;

    public int Suspicion { get => suspicion; }
    public int SusMary { get => suspicionMary; }
    public int SusRobin { get => suspicionRobin; }

    private float sliderLength;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this); 
            return; 
        }
        else Instance = this;

        RectTransform rect = GetComponent<RectTransform>();
        sliderLength = rect.sizeDelta.x;
        Debug.Log("Slider length = " + sliderLength);
    }

    public void ShowSlider(bool active)
    {
        susOverfill.gameObject.SetActive(false);
        susMeter.gameObject.SetActive(active);
    }

    public void UpdateSusValues(int player, int mary, int robin)
    {
        suspicionMary += mary;
        suspicionRobin += robin;
        if (player == 0) return;

        if (suspicion < susMeter.maxValue)
        {
            if (suspicion + player > susMeter.maxValue)
            {
                int overfill = suspicion + player - Mathf.RoundToInt(susMeter.maxValue);
                suspicion = Mathf.RoundToInt(susMeter.maxValue);

                StartCoroutine(UpdateSlider(true, overfill)); //Initiate overfill
            }
            else
            {
                suspicion += player;
                StartCoroutine(UpdateSlider(false, 0));
            }
        }
        else
        {
            suspicion += player;
            StartCoroutine(OverfillSlider());
        }
    }

    private IEnumerator UpdateSlider(bool overfill, int overfillValue)
    {
        bool positiveChange = susMeter.value < suspicion;

        if (positiveChange)
        {
            while(susMeter.value < suspicion)
            {
                susMeter.value += changeSpeed;
                susFill.color = gradient.Evaluate(susMeter.value / susMeter.maxValue);
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            while (susMeter.value > suspicion)
            {
                susMeter.value -= changeSpeed;
                yield return new WaitForEndOfFrame();
            }
        }
        susMeter.value = suspicion;

        if (overfill)
        {
            susOverfill.gameObject.SetActive(true);
            susOverfill.color = gradient.Evaluate(1);
            suspicion += overfillValue;

            StartCoroutine(OverfillSlider());
        }
    }

    private IEnumerator OverfillSlider()
    {
        RectTransform overfillRect = susOverfill.GetComponent<RectTransform>();

        float targetLength = suspicion * sliderLength / susMeter.maxValue;
        float curLength = overfillRect.sizeDelta.x;

        while (curLength < targetLength)
        {
            curLength += changeSpeed * sliderLength / susMeter.maxValue;
            overfillRect.sizeDelta = new Vector2(curLength, 50);
            yield return new WaitForEndOfFrame();
        }

        overfillRect.sizeDelta = new Vector2(targetLength, 50);
    }
}
