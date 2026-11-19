
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

    public ExerciseData CurrentExercise
    {
        get { return currentExercise; }
    }

    // Iniciar um exercicio atraves do seu indice
    public void StartExercise(int index)
    {
        if (IsRunning)
        {
            Debug.LogWarning(
                "Ja existe um exercicio em andamento."
            );
            return;
        }

        if (index < 0 || index >= exercises.Count)
        {
            Debug.LogWarning(
                "Indice de exercicio invalido."
            );
            return;
        }

        currentExercise = exercises[index];

        if (currentExercise == null ||
            currentExercise.cycles <= 0 ||
            currentExercise.inhaleDuration <= 0 ||
            currentExercise.exhaleDuration <= 0)
        {
            Debug.LogWarning(
                "Configuracao do exercicio invalida."
            );
            return;
        }

        IsRunning = true;

        if (gameManager != null)
        {
            gameManager.StartGame();
        }

        exerciseCoroutine = StartCoroutine(
            ExerciseRoutine()
        );

        Debug.Log(
            "Exercicio iniciado: " +
            currentExercise.exerciseName
        );
    }

    // Controlar os ciclos respiratorios
    private IEnumerator ExerciseRoutine()
    {
        for (int i = 0; i < currentExercise.cycles; i++)
        {
            Debug.Log(
                "Ciclo " + (i + 1) +
                " de " + currentExercise.cycles
            );

            Debug.Log("INSPIRAR");

            yield return new WaitForSeconds(
                currentExercise.inhaleDuration
            );

            Debug.Log("EXPIRAR");

            yield return new WaitForSeconds(
                currentExercise.exhaleDuration
            );
        }

        Debug.Log("Exercicio concluido!");

        IsRunning = false;
        exerciseCoroutine = null;

        if (gameManager != null)
        {
            gameManager.ReturnToMenu();
        }
    }

    // Interromper o exercicio
    public void StopExercise()
    {
        if (!IsRunning)
        {
            return;
        }

        if (exerciseCoroutine != null)
        {
            StopCoroutine(exerciseCoroutine);
            exerciseCoroutine = null;
        }

        IsRunning = false;

        if (gameManager != null)
        {
            gameManager.ReturnToMenu();
        }

        Debug.Log("Exercicio interrompido!");
    }

    // Controlos temporarios para testes
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartExercise(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartExercise(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartExercise(2);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopExercise();
        }
    }
}
