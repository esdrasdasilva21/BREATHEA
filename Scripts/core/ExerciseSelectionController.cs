
using UnityEngine;
using UnityEngine.UI;

public class ExerciseSelectionController : MonoBehaviour
{
    [Header("References")]
    public ExerciseManager exerciseManager;
    public UITransitionController transitionController;

    [Header("Selection Panel")]
    public GameObject selectionPanel;
    public CanvasGroup selectionCanvasGroup;

    [Header("Exercise Buttons")]
    public Button exercise1Button;
    public Button exercise2Button;
    public Button exercise3Button;
    public Button startExerciseButton;
    public Button clearSelectionButton;

    [Header("Description")]
    public Text exerciseDescription;

    [Header("Selection Colors")]
    public Color normalColor = Color.white;
    public Color selectedColor = new Color(0.55f, 0.85f, 0.65f);

    private int selectedExerciseIndex = -1;
    private Button[] exerciseButtons;

    private void Start()
    {
        exerciseButtons = new Button[]
        {
            exercise1Button,
            exercise2Button,
            exercise3Button
        };

        exercise1Button.onClick.AddListener(() => SelectExercise(0));
        exercise2Button.onClick.AddListener(() => SelectExercise(1));
        exercise3Button.onClick.AddListener(() => SelectExercise(2));

        startExerciseButton.onClick.AddListener(StartSelectedExercise);
        clearSelectionButton.onClick.AddListener(ClearSelection);

        startExerciseButton.interactable = false;
        exerciseDescription.text = "Seleciona um exercício";

        UpdateButtonColors();
    }

    private void OnEnable()
    {
        if (exerciseManager != null)
            exerciseManager.OnExerciseStopped += ShowSelectionPanel;
    }

    private void OnDisable()
    {
        if (exerciseManager != null)
            exerciseManager.OnExerciseStopped -= ShowSelectionPanel;
    }

    private void SelectExercise(int index)
    {
        if (index < 0 || index >= exerciseManager.exercises.Count)
            return;

        ExerciseData exercise = exerciseManager.exercises[index];

        selectedExerciseIndex = index;

        exerciseDescription.text =
            exercise.exerciseName + "\n\n" +
            "Inspirar: " + exercise.inhaleDuration + " segundos\n" +
            "Expirar: " + exercise.exhaleDuration + " segundos\n" +
            "Ciclos: " + exercise.cycles;

        startExerciseButton.interactable = true;

        UpdateButtonColors();
    }

    private void ClearSelection()
    {
        selectedExerciseIndex = -1;

        exerciseDescription.text = "Seleciona um exercício";
        startExerciseButton.interactable = false;

        UpdateButtonColors();
    }

    private void UpdateButtonColors()
    {
        for (int i = 0; i < exerciseButtons.Length; i++)
        {
            ColorBlock colors = exerciseButtons[i].colors;

            colors.normalColor = (i == selectedExerciseIndex)
                ? selectedColor
                : normalColor;

            exerciseButtons[i].colors = colors;
        }
    }

    private void StartSelectedExercise()
    {
        if (selectedExerciseIndex < 0 || exerciseManager.IsRunning)
            return;

        transitionController.HidePanel(selectionCanvasGroup);

        exerciseManager.StartExercise(selectedExerciseIndex);
    }

    private void ShowSelectionPanel()
    {
        transitionController.ShowPanel(selectionCanvasGroup);
    }
}
