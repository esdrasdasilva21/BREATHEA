
using System.Collections;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    [Header("References")]
    public ExerciseManager exerciseManager;

    [Header("Environment Objects")]
    public Transform[] natureObjects;

    [Header("Animation Settings")]
    public float inhaleScaleMultiplier = 1.15f;

    public float animationSpeed = 1f;

    private Vector3[] originalScales;

    private Coroutine environmentCoroutine;

    private void Awake()
    {
        StoreOriginalScales();
    }

    private void OnEnable()
    {
        if (exerciseManager == null)
            return;

        exerciseManager.OnInhaleStarted += HandleInhale;

        exerciseManager.OnExhaleStarted += HandleExhale;

        exerciseManager.OnExerciseFinished += HandleFinished;

        exerciseManager.OnExerciseStopped += HandleStopped;
    }

    private void OnDisable()
    {
        if (exerciseManager == null)
            return;

        exerciseManager.OnInhaleStarted -= HandleInhale;

        exerciseManager.OnExhaleStarted -= HandleExhale;

        exerciseManager.OnExerciseFinished -= HandleFinished;

        exerciseManager.OnExerciseStopped -= HandleStopped;
    }

    // Guardar os tamanhos originais
    private void StoreOriginalScales()
    {
        originalScales = new Vector3[natureObjects.Length];

        for (int i = 0; i < natureObjects.Length; i++)
        {
            if (natureObjects[i] != null)
            {
                originalScales[i] =
                    natureObjects[i].localScale;
            }
        }
    }

    // Reagir a inspiracao
    private void HandleInhale(float duration)
    {
        AnimateEnvironment(
            inhaleScaleMultiplier,
            duration
        );

        Debug.Log("Ambiente: Inspiracao");
    }

    // Reagir a expiracao
    private void HandleExhale(float duration)
    {
        AnimateEnvironment(
            1f,
            duration
        );

        Debug.Log("Ambiente: Expiracao");
    }

    // Quando o exercicio termina
    private void HandleFinished()
    {
        AnimateEnvironment(1f, animationSpeed);
    }

    // Quando o exercicio e interrompido
    private void HandleStopped()
    {
        AnimateEnvironment(1f, animationSpeed);
    }

    // Iniciar animacao do ambiente
    private void AnimateEnvironment(
        float multiplier,
        float duration)
    {
        if (environmentCoroutine != null)
        {
            StopCoroutine(environmentCoroutine);
        }

        environmentCoroutine = StartCoroutine(
            EnvironmentAnimation(
                multiplier,
                duration
            )
        );
    }

    // Animar todos os elementos naturais
    private IEnumerator EnvironmentAnimation(
        float multiplier,
        float duration)
    {
        Vector3[] startingScales =
            new Vector3[natureObjects.Length];

        Vector3[] targetScales =
            new Vector3[natureObjects.Length];

        for (int i = 0; i < natureObjects.Length; i++)
        {
            if (natureObjects[i] == null)
                continue;

            startingScales[i] =
                natureObjects[i].localScale;

            targetScales[i] =
                originalScales[i] * multiplier;
        }

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float progress = Mathf.Clamp01(
                elapsed / duration
            );

            float smoothProgress =
                Mathf.SmoothStep(0f, 1f, progress);

            for (int i = 0; i < natureObjects.Length; i++)
            {
                if (natureObjects[i] == null)
                    continue;

                natureObjects[i].localScale =
                    Vector3.Lerp(
                        startingScales[i],
                        targetScales[i],
                        smoothProgress
                    );
            }

            yield return null;
        }

        for (int i = 0; i < natureObjects.Length; i++)
        {
            if (natureObjects[i] == null)
                continue;

            natureObjects[i].localScale =
                targetScales[i];
        }

        environmentCoroutine = null;
    }
}
