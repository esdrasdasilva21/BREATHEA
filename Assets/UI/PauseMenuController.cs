
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [Header("References")]
    public ExerciseManager exerciseManager;
    public GameObject pausePanel;

    private void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (exerciseManager.IsPaused)
            {
                ResumeGame();
            }
            else if (exerciseManager.IsRunning)
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        if (exerciseManager != null)
        {
            exerciseManager.PauseExercise();

            if (pausePanel != null)
                pausePanel.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        if (exerciseManager != null)
        {
            exerciseManager.ResumeExercise();

            if (pausePanel != null)
                pausePanel.SetActive(false);
        }
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

        if (pausePanel != null)
            pausePanel.SetActive(false);
    }
}
