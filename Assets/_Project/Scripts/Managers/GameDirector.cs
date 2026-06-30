using System;
using Unity.AI.Navigation;
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
        LoadPersistanceData();
        uiManager.ShowMainMenu();
    }

    private void LoadPersistanceData()
    {
        levelManager.levelNo = PlayerPrefs.GetInt("HighestLevelCompleted") + 1;
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
        uiManager.ShowInGameUI();
        uiManager.levelNoUI.SetLevelNo(levelManager.levelNo);
        if(levelManager.levelNo == 1)
        {
            uiManager.messageUI.Show("WASD To Move!", 2f);
        }
    }

    public void LevelCompleted()
    {
        gameState = GameState.WinUI;
        if (levelManager.levelNo == levelManager.GetLevelsCount())
        {
            uiManager.ShowCreditsUI();
        }
        else
        {
            uiManager.ShowVictoryUI();
        }
        PlayerPrefs.SetInt("HighestLevelCompleted", levelManager.levelNo);
        levelManager.GetCurrentLevel().GetComponent<NavMeshSurface>().RemoveData();
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