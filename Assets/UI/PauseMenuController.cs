
using System.Collections;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [Header("References")]
    public ExerciseManager exerciseManager;
    public GameObject pausePanel;
    public CanvasGroup pauseCanvasGroup;

    [Header("Animation Settings")]
    public float transitionDuration = 0.35f;

    private Coroutine transitionCoroutine;

    private void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);

        if (pauseCanvasGroup != null)
            pauseCanvasGroup.alpha = 0f;
    }

    public void PauseGame()
    {
        if (exerciseManager == null ||
            !exerciseManager.IsRunning ||
            exerciseManager.IsPaused)
            return;

        exerciseManager.PauseExercise();

        if (pausePanel != null)
            pausePanel.SetActive(true);

        StartTransition(1f, true);
    }

    public void ResumeGame()
    {
        if (exerciseManager == null ||
            !exerciseManager.IsPaused)
            return;

        exerciseManager.ResumeExercise();

        StartTransition(0f, false);
    }

    public void RestartGame()
    {
        if (exerciseManager == null)
            return;

        int currentIndex =
            exerciseManager.CurrentExercise != null
            ? exerciseManager.exercises.IndexOf(
                exerciseManager.CurrentExercise)
            : -1;

        if (currentIndex < 0)
            return;

        exerciseManager.StopExercise();

        if (pausePanel != null)
            pausePanel.SetActive(false);

        exerciseManager.StartExercise(currentIndex);
    }

    public void ExitPauseMenu()
    {
        if (exerciseManager != null)
            exerciseManager.StopExercise();

        StartTransition(0f, false);
    }

    private void StartTransition(float targetAlpha, bool interactable)
    {
        if (transitionCoroutine != null)
            StopCoroutine(transitionCoroutine);

        transitionCoroutine = StartCoroutine(
            FadePanel(targetAlpha, interactable)
        );
    }

    private IEnumerator FadePanel(float targetAlpha, bool interactable)
    {
        if (pauseCanvasGroup == null)
            yield break;

        pauseCanvasGroup.interactable = false;
        pauseCanvasGroup.blocksRaycasts = true;

        float startAlpha = pauseCanvasGroup.alpha;
        float elapsed = 0f;

        while (elapsed < transitionDuration)
        {
            elapsed += Time.unscaledDeltaTime;

            float progress = Mathf.Clamp01(
                elapsed / transitionDuration
            );

            pauseCanvasGroup.alpha = Mathf.Lerp(
                startAlpha,
                targetAlpha,
                progress
            );

            yield return null;
        }

        pauseCanvasGroup.alpha = targetAlpha;

        pauseCanvasGroup.interactable = interactable;
        pauseCanvasGroup.blocksRaycasts = interactable;

        if (!interactable && pausePanel != null)
            pausePanel.SetActive(false);

        transitionCoroutine = null;
    }
}
