
using UnityEngine;
using UnityEngine.UI;

public class StatisticsController : MonoBehaviour
{
    [Header("References")]
    public SessionHistoryManager historyManager;
    public GameObject statisticsPanel;
    public GameObject selectionPanel;

    [Header("UI")]
    public Text totalSessionsText;
    public Text totalCyclesText;
    public Text totalDurationText;
    public Text lastSessionText;
    public Button backButton;

    private void Start()
    {
        statisticsPanel.SetActive(false);
        backButton.onClick.AddListener(CloseStatistics);
    }

    public void OpenStatistics()
    {
        UpdateStatistics();

        statisticsPanel.SetActive(true);
        selectionPanel.SetActive(false);
    }

    private void UpdateStatistics()
    {
        totalSessionsText.text =
            "Sessões: " + historyManager.GetTotalSessions();

        totalCyclesText.text =
            "Ciclos: " + historyManager.GetTotalCycles();

        int seconds = Mathf.RoundToInt(
            historyManager.GetTotalDuration()
        );

        totalDurationText.text =
            "Tempo acumulado: " + seconds + "s";

        if (historyManager.sessions.Count > 0)
        {
            SessionData lastSession =
                historyManager.sessions[
                    historyManager.sessions.Count - 1
                ];

            lastSessionText.text =
                "Última sessão: " +
                lastSession.exerciseName + "\n" +
                lastSession.completionDate;
        }
        else
        {
            lastSessionText.text = "Última sessão: Nenhuma";
        }
    }

    private void CloseStatistics()
    {
        statisticsPanel.SetActive(false);
        selectionPanel.SetActive(true);
    }
}
