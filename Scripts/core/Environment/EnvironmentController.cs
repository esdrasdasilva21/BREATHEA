
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ExerciseUIController : MonoBehaviour
{
    [Header("References")]
    public ExerciseManager exerciseManager;

    [Header("UI Elements")]
    public Text exerciseTitle;
    public Text timerText;
    public Text cycleText;

    private Coroutine timerCoroutine;

    private void OnEnable()
    {
        if (exerciseManager == null)
            return;

        exerciseManager.OnInhaleStarted += HandlePhaseStarted;
        exerciseManager.OnExhaleStarted += HandlePhaseStarted;

        exerciseManager.OnCycleCompleted += HandleCycleCompleted;

        exerciseManager.OnExerciseFinished += HandleExerciseFinished;
        exerciseManager.OnExerciseStopped += HandleExerciseStopped;
    }

    private void OnDisable()
    {
        if (exerciseManager == null)
            return;

        exerciseManager.OnInhaleStarted -= HandlePhaseStarted;
        exerciseManager.OnExhaleStarted -= HandlePhaseStarted;

        exerciseManager.OnCycleCompleted -= HandleCycleCompleted;

        exerciseManager.OnExerciseFinished -= HandleExerciseFinished;
        exerciseManager.OnExerciseStopped -= HandleExerciseStopped;
    }

    private void HandlePhaseStarted(float duration)
    {
        if (exerciseManager.CurrentExercise != null &&
            exerciseTitle != null)
        {
            exerciseTitle.text =
                exerciseManager.CurrentExercise.exerciseName;
        }

        if (timerCoroutine != null)
            StopCoroutine(timerCoroutine);

        timerCoroutine = StartCoroutine(UpdateTimer(duration));
    }

    private IEnumerator UpdateTimer(float duration)
    {
        float remainingTime = duration;

        while (remainingTime > 0f)
        {
            if (timerText != null)
            {
                timerText.text =
                    remainingTime.ToString("F1") + " s";
            }

            remainingTime -= Time.deltaTime;

            yield return null;
        }

        if (timerText != null)
            timerText.text = "0.0 s";

        timerCoroutine = null;
    }

    private void HandleCycleCompleted(int completed, int total)
    {
        if (cycleText != null)
        {
            cycleText.text =
                "CICLO " + completed + " / " + total;
        }
    }

    private void HandleExerciseFinished()
    {
        ResetUI("EXERCÍCIO CONCLUÍDO");
    }

    private void HandleExerciseStopped()
    {
        ResetUI("EXERCÍCIO INTERROMPIDO");
    }

    private void ResetUI(string message)
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }

        if (exerciseTitle != null)
            exerciseTitle.text = message;

        if (timerText != null)
            timerText.text = "--";
    }
}
