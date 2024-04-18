using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VerdictAnimation : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI you;
    [SerializeField] private TextMeshProUGUI guilty;
    [SerializeField] private TextMeshProUGUI notGuilty;
    [Space(10)]
    [SerializeField] private List<GameObject> confetti = new();

    [Header("Parameters")]
    [SerializeField] private float notGuiltyTiming;
    [SerializeField] private float guiltyTiming;

    private Animation _anim;
    private AudioSource _audioSource;

    private void Awake()
    {
        _anim = GetComponent<Animation>();
        _audioSource = GetComponent<AudioSource>();

        you.gameObject.SetActive(false);
        guilty.gameObject.SetActive(false);
        notGuilty.gameObject.SetActive(false);
    }

    public void PlayGuilty() => StartCoroutine(GuiltyAnimation());
    public void PlayNotGuilty() => StartCoroutine(NotGuiltyAnimation());

    private IEnumerator GuiltyAnimation()
    {
        you.gameObject.SetActive(true);
        guilty.gameObject.SetActive(true);
        notGuilty.gameObject.SetActive(false);
        guilty.text = string.Empty;

        _anim.Play();
        yield return new WaitForSeconds(_anim.clip.length);

        for (int i = 1; i <= 6; i++)
        {
            _audioSource.Play();
            guilty.text = "Guilty"[..i];
            yield return new WaitForSeconds(guiltyTiming);
        }
    }

    private IEnumerator NotGuiltyAnimation()
    {
        you.gameObject.SetActive(true);
        guilty.gameObject.SetActive(false);
        notGuilty.gameObject.SetActive(true);
        notGuilty.text = string.Empty;

        _anim.Play();
        yield return new WaitForSeconds(_anim.clip.length);

        notGuilty.text = "Not";
        yield return new WaitForSeconds(notGuiltyTiming);
        notGuilty.text = "Not guilty";
        yield return new WaitForSeconds(notGuiltyTiming);

        //Confetti time
        foreach (GameObject o in confetti)
        {
            o.SetActive(true);
        }
    }
}
