using UnityEngine;
using UnityEngine.UI; // For Image
using TMPro;
using System.Collections;

public class textboxManager : MonoBehaviour
{
    public CanvasGroup panelCanvasGroup; // Add this to your panel
    public TMP_Text headerText;
    public TMP_Text bodyText;

    public CanvasGroup bodyCanvasGroup;
    public float fadeDuration = 0.5f; // seconds

    private Coroutine currentFade;

    void Awake()
    {
        if (panelCanvasGroup == null)
        {
            panelCanvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    /// <summary>
    /// Sets text and fades panel in
    /// </summary>
    public void ShowPanel(string header, string body)
    {
        if (currentFade != null)
            StopCoroutine(currentFade);

        headerText.text = header;
        bodyText.text = body;

        // Force layout update so panel resizes before fade-in
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);

        currentFade = StartCoroutine(FadeCanvasGroup(0f, 1f, fadeDuration, panelCanvasGroup));
    }

    /// <summary>
    /// Fades panel out
    /// </summary>
    public void HidePanel()
    {
        if (currentFade != null)
            StopCoroutine(currentFade);

        currentFade = StartCoroutine(FadeCanvasGroup(panelCanvasGroup.alpha, 0f, fadeDuration, panelCanvasGroup));
    }

    public void ShowBody(string body)
    {
        if (currentFade != null)
            StopCoroutine(currentFade);

        bodyText.text = body;

        // Force layout update so panel resizes before fade-in
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);

        currentFade = StartCoroutine(FadeCanvasGroup(0f, 1f, fadeDuration, bodyCanvasGroup));
    }

    public void HideBody()
    {
        if (currentFade != null)
            StopCoroutine(currentFade);

        currentFade = StartCoroutine(FadeCanvasGroup(bodyCanvasGroup.alpha, 0f, fadeDuration, bodyCanvasGroup));
    }

    private IEnumerator FadeCanvasGroup(float startAlpha, float endAlpha, float duration, CanvasGroup canvasGroup)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            yield return null;
        }
        canvasGroup.alpha = endAlpha;
    }
}