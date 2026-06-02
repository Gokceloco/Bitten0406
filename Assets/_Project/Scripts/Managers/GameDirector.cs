using System;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public UIManager uiManager;
    void Start()
    {
        uiManager.ShowMainMenu();
    }
    public void RestartLevel()
    {

    }    
}
