
using System.Collections.Generic;
using UnityEngine;

public class SessionHistoryManager : MonoBehaviour
{
    public List<SessionData> sessions = new List<SessionData>();

    public void AddSession(SessionData session)
    {
        if (session == null)
            return;

        sessions.Add(session);

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
        {
            total += session.completedCycles;
        }

        return total;
    }

    public float GetTotalDuration()
    {
        float total = 0f;

        foreach (SessionData session in sessions)
        {
            total += session.duration;
        }

        return total;
    }
}
