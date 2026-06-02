using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameDirector : MonoBehaviour
{
    public UIManager uiManager;
    public LevelManager levelManager;
    public Player player;

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

    private void LoadNextLevel()
    {
        levelManager.levelNo++;
        RestartLevel();
    }

    public void RestartLevel()
    {
        levelManager.RestartLevelManager();
        player.RestartPlayer();
    }    
}
