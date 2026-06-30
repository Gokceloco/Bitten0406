using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameDirector gameDirector;
    public int levelNo;

    [SerializeField] private List<Level> levels;

    private Level _currentLevel;

    public void RestartLevelManager()
    {
        levelNo = Mathf.Clamp(levelNo, 1, levels.Count);
        DeleteOldLevel();
        CreateNewLevel();
    }

    private void CreateNewLevel()
    {
        _currentLevel = Instantiate(levels[levelNo - 1], transform);
    }

    private void DeleteOldLevel()
    {
        if (_currentLevel != null)
        {
            Destroy(_currentLevel.gameObject);
        }
    }

    public Level ReturnCurrentLevel()
    {        
        return _currentLevel;
    }

    internal Level GetCurrentLevel()
    {
        return _currentLevel;
    }

    internal int GetLevelsCount()
    {
        return levels.Count;
    }
}
