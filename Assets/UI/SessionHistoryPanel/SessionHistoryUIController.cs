
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class SessionHistoryUIController : MonoBehaviour
{
    [Header("References")]
    public SessionHistoryManager historyManager;
    public GameObject historyPanel;
    public GameObject selectionPanel;

    [Header("UI Texts")]
    public Text totalSessionsText;
    public Text totalCyclesText;
    public Text totalDurationText;
    public Text sessionListText;

    [Header("Buttons")]
    public Button backButton;

    private void Start()
    {
        historyPanel.SetActive(false);

        backButton.onClick.AddListener(CloseHistory);
    }

    public void OpenHistory()
    {
        UpdateHistory();
        historyPanel.SetActive(true);
        selectionPanel.SetActive(false);
    }

    private void UpdateHistory()
    {
        totalSessionsText.text =
            "Sessões realizadas: " + historyManager.GetTotalSessions();

        totalCyclesText.text =
            "Ciclos concluídos: " + historyManager.GetTotalCycles();

        totalDurationText.text =
            "Tempo total: " +
            Mathf.RoundToInt(historyManager.GetTotalDuration()) +
            " segundos";

        StringBuilder history = new StringBuilder();

        if (historyManager.sessions.Count == 0)
        {
            history.Append("Ainda não existem sessões.");
        }
        else
        {
            foreach (SessionData session in historyManager.sessions)
            {
                history.Append("Exercício: ");
                history.AppendLine(session.exerciseName);

                history.Append("Ciclos: ");
                history.AppendLine(session.completedCycles.ToString());

                history.Append("Duração: ");
                history.AppendLine(
                    Mathf.RoundToInt(session.duration) + " segundos"
                );

                history.Append("Data: ");
                history.AppendLine(session.completionDate);

                history.AppendLine("----------------------");
            }
        }

        sessionListText.text = history.ToString();
    }

    private void CloseHistory()
    {
        historyPanel.SetActive(false);
        selectionPanel.SetActive(true);
    }
}
