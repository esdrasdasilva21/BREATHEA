
using System.Collections.Generic;
using UnityEngine;

public class SessionHistoryManager : MonoBehaviour
{
    public List<SessionData> sessions = new List<SessionData>();

    private void Awake()
    {
        sessions = SessionStorage.LoadSessions();

        Debug.Log("Sessões carregadas: " + sessions.Count);
    }

    public void AddSession(SessionData session)
    {
        if (session == null)
            return;

        sessions.Add(session);

        SessionStorage.SaveSessions(sessions);

        Debug.Log("Sessão registada: " + session.exerciseName);
    }

    public int GetTotalSessions()
    {
        return sessions.Count;
    }

    public int GetTotalCycles()
    {
        int total = 0;

        foreach (SessionData session in sessions)
            total += session.completedCycles;

        return total;
    }

    public float GetTotalDuration()
    {
        float total = 0f;

        foreach (SessionData session in sessions)
            total += session.duration;

        return total;
    }
}

using System.Collections.Generic;
using UnityEngine;

public class SessionHistoryManager : MonoBehaviour
{
    public List<SessionData> sessions = new List<SessionData>();

    private void Awake()
    {
        sessions = SessionStorage.LoadSessions();

        Debug.Log("Sessões carregadas: " + sessions.Count);
    }

    public void AddSession(SessionData session)
    {
        if (session == null)
            return;

        sessions.Add(session);

        SessionStorage.SaveSessions(sessions);

        Debug.Log("Sessão registada: " + session.exerciseName);
    }

    public int GetTotalSessions()
    {
        return sessions.Count;
    }

    public int GetTotalCycles()
    {
        int total = 0;

        foreach (SessionData session in sessions)
            total += session.completedCycles;

        return total;
    }

    public float GetTotalDuration()
    {
        float total = 0f;

        foreach (SessionData session in sessions)
            total += session.duration;

        return total;
    }
}
