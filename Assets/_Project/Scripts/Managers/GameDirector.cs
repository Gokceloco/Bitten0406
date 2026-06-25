using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameDirector : MonoBehaviour
{
    public UIManager uiManager;
    public LevelManager levelManager;
    public Player player;

    public AudioManager audioManager;
    public FXManager fXManager;

    public GameState gameState;

    public TimerManager timerManager;

    void Start()
    {
        uiManager.ShowMainMenu();
    }

    private void Update()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            LoadNextLevel();
        }
        if (Keyboard.current.oKey.wasPressedThisFrame)
        {
            LoadPreviousLevel();
        }
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            RestartLevel();
        }
    }

    private void LoadPreviousLevel()
    {
        levelManager.levelNo--;
        RestartLevel();
    }

    public void LoadNextLevel()
    {
        levelManager.levelNo++;
        RestartLevel();
    }

    public void RestartLevel()
    {
        gameState = GameState.GamePlay;
        levelManager.RestartLevelManager();
        player.RestartPlayer();
        timerManager.StartTimer(levelManager.ReturnCurrentLevel().levelTime);
    }    

    public void LevelCompleted()
    {
        gameState = GameState.WinUI;
        uiManager.ShowVictoryUI();
    }
    public void LevelFailed()
    {
        gameState = GameState.FailUI;
        uiManager.ShowFailUI();
    }
}

public enum GameState
{
    MainMenu,
    GamePlay,
    FailUI,
    WinUI,
}