
using System.Collections;
using UnityEngine;

public class UITransitionController : MonoBehaviour
{
    [Header("Transition Settings")]
    public float transitionDuration = 0.35f;

    private Coroutine currentTransition;

    public void ShowPanel(CanvasGroup canvasGroup)
    {
        if (canvasGroup == null)
            return;

        if (currentTransition != null)
            StopCoroutine(currentTransition);

        currentTransition = StartCoroutine(
            FadePanel(canvasGroup, 0f, 1f, true)
        );
    }

    public void HidePanel(CanvasGroup canvasGroup)
    {
        if (canvasGroup == null)
            return;

        if (currentTransition != null)
            StopCoroutine(currentTransition);

        currentTransition = StartCoroutine(
            FadePanel(canvasGroup, 1f, 0f, false)
        );
    }

    private IEnumerator FadePanel(
        CanvasGroup canvasGroup,
        float startAlpha,
        float targetAlpha,
        bool show)
    {
        if (show)
            canvasGroup.gameObject.SetActive(true);

        canvasGroup.alpha = startAlpha;

        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        float elapsed = 0f;

        while (elapsed < transitionDuration)
        {
            elapsed += Time.unscaledDeltaTime;

            float progress = Mathf.Clamp01(
                elapsed / transitionDuration
            );

            canvasGroup.alpha = Mathf.Lerp(
                startAlpha,
                targetAlpha,
                progress
            );

            yield return null;
        }

        canvasGroup.alpha = targetAlpha;

        canvasGroup.interactable = show;
        canvasGroup.blocksRaycasts = show;

        if (!show)
            canvasGroup.gameObject.SetActive(false);

        currentTransition = null;
    }
}
