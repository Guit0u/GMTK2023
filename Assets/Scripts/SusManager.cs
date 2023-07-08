using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SusManager : MonoBehaviour
{
    public static SusManager Instance;

    [SerializeField] private Slider susMeter;
    [SerializeField] private Image susFill;
    [SerializeField] private Gradient gradient;

    [Header("Parameters")]
    [SerializeField, Range(0, 1)] private float changeSpeed;
    [SerializeField, Range(0, 1)] private float pauseTime;

    [Header("Debug")]
    [SerializeField] private int suspicion = 0;
    public int Suspicion { get => suspicion; }

    private void Awake()
    {
        if (Instance != null) Destroy(this);
        else Instance = this;
    }

    public void ChangeSus(int value)
    {
        suspicion += value;
        StartCoroutine(UpdateSlider());
    }

    private IEnumerator UpdateSlider()
    {
        susMeter.gameObject.SetActive(true);

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

        yield return new WaitForSeconds(pauseTime);

        susMeter.gameObject.SetActive(false);
    }
}
