
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseManager : MonoBehaviour
{
    [Header("Exercise Configuration")]
    public List<ExerciseData> exercises =
        new List<ExerciseData>();

    [Header("Game References")]
    public GameManager gameManager;

    private Coroutine exerciseCoroutine;
    private ExerciseData currentExercise;

    public bool IsRunning { get; private set; }
    public bool IsPaused { get; private set; }

    public ExerciseData CurrentExercise
    {
        get { return currentExercise; }
    }

    public event Action<float> OnInhaleStarted;
    public event Action<float> OnExhaleStarted;
    public event Action<int, int> OnCycleCompleted;
    public event Action OnExerciseFinished;
    public event Action OnExerciseStopped;

    public void StartExercise(int index)
    {
        if (IsRunning)
        {
            Debug.LogWarning("Ja existe um exercicio em andamento.");
            return;
        }

        if (index < 0 || index >= exercises.Count)
        {
            Debug.LogWarning("Indice de exercicio invalido.");
            return;
        }

        currentExercise = exercises[index];

        if (currentExercise == null ||
            currentExercise.cycles <= 0 ||
            currentExercise.inhaleDuration <= 0 ||
            currentExercise.exhaleDuration <= 0)
        {
            Debug.LogWarning("Configuracao do exercicio invalida.");
            return;
        }

        IsRunning = true;
        IsPaused = false;

        if (gameManager != null)
            gameManager.StartGame();

        exerciseCoroutine = StartCoroutine(ExerciseRoutine());

        Debug.Log("Exercicio iniciado: " + currentExercise.exerciseName);
    }

    private IEnumerator ExerciseRoutine()
    {
        for (int i = 0; i < currentExercise.cycles; i++)
        {
            Debug.Log(
                "Ciclo " + (i + 1) +
                " de " + currentExercise.cycles
            );

            Debug.Log("INSPIRAR");

            OnInhaleStarted?.Invoke(
                currentExercise.inhaleDuration
            );

            yield return new WaitForSeconds(
                currentExercise.inhaleDuration
            );

            Debug.Log("EXPIRAR");

            OnExhaleStarted?.Invoke(
                currentExercise.exhaleDuration
            );

            yield return new WaitForSeconds(
                currentExercise.exhaleDuration
            );

            OnCycleCompleted?.Invoke(
                i + 1,
                currentExercise.cycles
            );
        }

        IsRunning = false;
        IsPaused = false;
        exerciseCoroutine = null;

        Debug.Log("Exercicio concluido!");

        OnExerciseFinished?.Invoke();

        if (gameManager != null)
            gameManager.ReturnToMenu();
    }

    public void PauseExercise()
    {
        if (!IsRunning || IsPaused)
            return;

        IsPaused = true;

        if (gameManager != null)
            gameManager.PauseGame();

        Debug.Log("Exercicio pausado!");
    }

    public void ResumeExercise()
    {
        if (!IsRunning || !IsPaused)
            return;

        IsPaused = false;

        if (gameManager != null)
            gameManager.ResumeGame();

        Debug.Log("Exercicio retomado!");
    }

    public void StopExercise()
    {
        if (!IsRunning)
            return;

        if (exerciseCoroutine != null)
        {
            StopCoroutine(exerciseCoroutine);
            exerciseCoroutine = null;
        }

        IsRunning = false;
        IsPaused = false;

        Time.timeScale = 1f;

        OnExerciseStopped?.Invoke();

        if (gameManager != null)
            gameManager.ReturnToMenu();

        Debug.Log("Exercicio interrompido!");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            StartExercise(0);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            StartExercise(1);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            StartExercise(2);

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (IsPaused)
                ResumeExercise();
            else
                PauseExercise();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            StopExercise();
    }
}
