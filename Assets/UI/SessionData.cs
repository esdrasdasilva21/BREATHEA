
using System;

[Serializable]
public class SessionData
{
    public string exerciseName;
    public int completedCycles;
    public float duration;
    public string completionDate;

    public SessionData(
        string name,
        int cycles,
        float sessionDuration)
    {
        exerciseName = name;
        completedCycles = cycles;
        duration = sessionDuration;
        completionDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
    }
}
