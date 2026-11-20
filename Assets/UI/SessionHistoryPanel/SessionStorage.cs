
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SessionStorage
{
    private static string FilePath =>
        Path.Combine(Application.persistentDataPath, "session_history.json");

    [System.Serializable]
    private class SessionList
    {
        public List<SessionData> sessions = new List<SessionData>();
    }

    public static void SaveSessions(List<SessionData> sessions)
    {
        SessionList data = new SessionList();
        data.sessions = sessions;

        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(FilePath, json);

        Debug.Log("Histórico guardado em: " + FilePath);
    }

    public static List<SessionData> LoadSessions()
    {
        if (!File.Exists(FilePath))
            return new List<SessionData>();

        string json = File.ReadAllText(FilePath);

        SessionList data = JsonUtility.FromJson<SessionList>(json);

        if (data == null || data.sessions == null)
            return new List<SessionData>();

        return data.sessions;
    }
}
