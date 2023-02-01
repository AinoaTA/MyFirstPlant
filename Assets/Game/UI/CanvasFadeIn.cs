using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasFadeIn : MonoBehaviour
{
    public float Duration = 0.4f;
    public float DelayFadeIn = 0f;
    public float DelayFadeOut = 0f;
    private CanvasGroup canvGroup;
    public bool HideOnStart;

    public UnityEvent onShow;
    public UnityEvent onHide;

    public void Awake()
    {
        canvGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        if (HideOnStart)
        {
            canvGroup.alpha = 1f;
            canvGroup.interactable = false;
            canvGroup.blocksRaycasts = false;
            Hide();
        }
    }

    public void Show()
    {
        StartCoroutine(DoFade(canvGroup, canvGroup.alpha, 1, DelayFadeIn, onShow));
    }

    public void Hide()
    {
        StartCoroutine(DoFade(canvGroup, canvGroup.alpha, 0, DelayFadeOut, onHide));
    }

    public IEnumerator DoFade(CanvasGroup canvGroup, float start, float end, float delay, UnityEvent onFinish)
    {
        float counter = 0f;

        if (end >= 1)
        {
            canvGroup.interactable = true;
            canvGroup.blocksRaycasts = true;
        }
        else
        {
            canvGroup.interactable = false;
            canvGroup.blocksRaycasts = false;
        }

        yield return new WaitForSecondsRealtime(delay);

        while (counter < Duration)
        {
            counter += Time.unscaledDeltaTime;
            canvGroup.alpha = Mathf.Lerp(start, end, counter / Duration);

            yield return null;
        }

        onFinish.Invoke();
    }
}
