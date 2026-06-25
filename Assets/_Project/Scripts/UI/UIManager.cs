using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameDirector gameDirector;
    public MainMenu mainMenu;
    public FailUI failUI;
    public WinUI winUI;
    private void Start()
    {
        StartUI();
    }

    public void StartUI()
    {
        winUI.Hide();
        failUI.Hide();
    }

    public void ShowMainMenu()
    {
        gameDirector.gameState = GameState.MainMenu;
        mainMenu.Show();
    }

    public void StartGameButtonPressed()
    {
        mainMenu.Hide();
        gameDirector.RestartLevel();
    }

    public void ShowFailUI()
    {
        failUI.Show(2);
    }

    public void ShowVictoryUI()
    {
        winUI.Show(1);
    }
}
