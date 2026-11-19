
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        MainMenu,
        Playing,
        Paused
    }

    public GameState CurrentState { get; private set; }

    private void Awake()
    {
        CurrentState = GameState.MainMenu;
        Time.timeScale = 1f;
    }

    public void StartGame()
    {
        CurrentState = GameState.Playing;
        Debug.Log("Exercicio iniciado!");
    }

    public void PauseGame()
    {
        CurrentState = GameState.Paused;
        Time.timeScale = 0f;

        Debug.Log("Exercicio pausado!");
    }

    public void ResumeGame()
    {
        CurrentState = GameState.Playing;
        Time.timeScale = 1f;

        Debug.Log("Exercicio retomado!");
    }

    public void ReturnToMenu()
    {
        CurrentState = GameState.MainMenu;
        Time.timeScale = 1f;

        Debug.Log("Regressando ao menu!");
    }
}
