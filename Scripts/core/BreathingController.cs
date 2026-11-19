
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BreathingController : MonoBehaviour
{
    [Header("References")]
    public ExerciseManager exerciseManager;

    public Transform breathingSphere;

    public Text instructionText;

    [Header("Animation Settings")]
    public float minScale = 1f;
    public float maxScale = 2f;

    private Coroutine animationCoroutine;

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

    private void Start()
    {
        ResetSphere();
    }

    // Quando comeca a inspiracao
    private void HandleInhale(float duration)
    {
        SetInstruction("INSPIRAR");

        AnimateSphere(minScale, maxScale, duration);
    }

    // Quando comeca a expiracao
    private void HandleExhale(float duration)
    {
        SetInstruction("EXPIRAR");

        AnimateSphere(maxScale, minScale, duration);
    }

    // Quando o exercicio termina
    private void HandleFinished()
    {
        SetInstruction("EXERCICIO CONCLUIDO!");

        ResetSphere();
    }

    // Quando o exercicio e interrompido
    private void HandleStopped()
    {
        SetInstruction("EXERCICIO INTERROMPIDO");

        ResetSphere();
    }

    private void SetInstruction(string message)
    {
        if (instructionText != null)
        {
            instructionText.text = message;
        }
    }

    // Iniciar animacao da esfera
    private void AnimateSphere(
        float startScale,
        float endScale,
        float duration)
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }

        animationCoroutine = StartCoroutine(
            SphereAnimation(
                startScale,
                endScale,
                duration
            )
        );
    }

    // Animacao progressiva
    private IEnumerator SphereAnimation(
        float startScale,
        float endScale,
        float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float progress = Mathf.Clamp01(
                elapsed / duration
            );

            float smoothProgress =
                Mathf.SmoothStep(0f, 1f, progress);

            float currentScale = Mathf.Lerp(
                startScale,
                endScale,
                smoothProgress
            );

            breathingSphere.localScale =
                Vector3.one * currentScale;

            yield return null;
        }

        breathingSphere.localScale =
            Vector3.one * endScale;

        animationCoroutine = null;
    }

    // Restaurar tamanho original
    private void ResetSphere()
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
            animationCoroutine = null;
        }

        if (breathingSphere != null)
        {
            breathingSphere.localScale =
                Vector3.one * minScale;
        }
    }
}
