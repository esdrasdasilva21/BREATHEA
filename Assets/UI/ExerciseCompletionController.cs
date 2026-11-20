
using UnityEngine;
using UnityEngine.UI;

public class ExerciseCompletionController : MonoBehaviour
{
    [Header("References")]
    public ExerciseManager exerciseManager;
    public GameObject completionPanel;
    public GameObject selectionPanel;

    [Header("UI")]
    public Button backToSelectionButton;
    public Text exerciseNameText;
    public Text sessionStatsText;

    private int completedCycles;
    private float sessionDuration;
    private float sessionStartTime;

    private void Start()
    {
        completionPanel.SetActive(false);

        backToSelectionButton.onClick.AddListener(ReturnToSelection);
    }

    private void OnEnable()
    {
        if (exerciseManager == null)
            return;

        exerciseManager.OnExerciseStarted += StartSession;
        exerciseManager.OnCycleCompleted += RegisterCycle;
        exerciseManager.OnExerciseFinished += ShowCompletionPanel;
    }

    private void OnDisable()
    {
        if (exerciseManager == null)
            return;

        exerciseManager.OnExerciseStarted -= StartSession;
        exerciseManager.OnCycleCompleted -= RegisterCycle;
        exerciseManager.OnExerciseFinished -= ShowCompletionPanel;
    }

    private void StartSession()
    {
        completedCycles = 0;
        sessionDuration = 0f;
        sessionStartTime = Time.time;
    }

    private void RegisterCycle(int current, int total)
    {
        completedCycles = current;
    }

    private void ShowCompletionPanel()
    {
        sessionDuration = Time.time - sessionStartTime;

        exerciseNameText.text =
            "Exercício: " + exerciseManager.CurrentExercise.exerciseName;

        sessionStatsText.text =
            "Ciclos concluídos: " + completedCycles + "\n" +
            "Duração: " + Mathf.RoundToInt(sessionDuration) + " segundos";

        completionPanel.SetActive(true);
    }

    private void ReturnToSelection()
    {
        completionPanel.SetActive(false);
        selectionPanel.SetActive(true);
    }
}
