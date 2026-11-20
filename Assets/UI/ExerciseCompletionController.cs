
using UnityEngine;
using UnityEngine.UI;

public class ExerciseCompletionController : MonoBehaviour
{
    [Header("References")]
    public ExerciseManager exerciseManager;
    public GameObject completionPanel;
    public GameObject selectionPanel;
    public Button backToSelectionButton;

    private void Start()
    {
        completionPanel.SetActive(false);

        backToSelectionButton.onClick.AddListener(ReturnToSelection);
    }

    private void OnEnable()
    {
        if (exerciseManager != null)
            exerciseManager.OnExerciseFinished += ShowCompletionPanel;
    }

    private void OnDisable()
    { 
        if (exerciseManager != null)
            exerciseManager.OnExerciseFinished -= ShowCompletionPanel;
    }

    private void ShowCompletionPanel()
    {
        completionPanel.SetActive(true);
    }

    private void ReturnToSelection()
    {
        completionPanel.SetActive(false);
        selectionPanel.SetActive(true);
    }
}
