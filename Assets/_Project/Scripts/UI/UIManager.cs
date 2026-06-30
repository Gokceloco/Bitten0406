using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameDirector gameDirector;
    public MainMenu mainMenu;
    public FailUI failUI;
    public WinUI winUI;

    public TimerUI timerUI;
    public LevelNoUI levelNoUI;

    public MessageUI messageUI;

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
        HideInGameUI();
    }

    public void ShowInGameUI()
    {
        timerUI.Show();
        levelNoUI.Show();
    }

    public void HideInGameUI()
    {
        timerUI.Hide();
        levelNoUI.Hide();
        messageUI.Hide();
    }

    public void StartGameButtonPressed()
    {
        mainMenu.Hide();
        gameDirector.RestartLevel();
    }

    public void ShowFailUI()
    {
        failUI.Show(2);
        HideInGameUI();
    }

    public void ShowVictoryUI()
    {
        winUI.Show(1);
        HideInGameUI();
    }

    internal void ShowCreditsUI()
    {
        throw new NotImplementedException();
    }
}
